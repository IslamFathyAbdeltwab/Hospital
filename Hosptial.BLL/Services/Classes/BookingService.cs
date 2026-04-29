using AutoMapper;
using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.Specification;
using Hosptial.BLL.ViewModels.BookingViewModels;
using Hosptial.BLL.ViewModels.PatientViewModels;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Interfaces;
using Stripe;
using Microsoft.IdentityModel.Tokens.Experimental;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Classes
{
    public class BookingService : IBookingService
    {
        private readonly IGenaricRepo<Booking> _bookingRepo;

        private readonly IUniteOfWork _unitOfWork;
        private readonly IGenaricRepo<DoctorAvailability> availabilityRepo;
        private readonly IMapper _mapper;

        public BookingService(
            IGenaricRepo<Booking> bookingRepo,
            IUniteOfWork unitOfWork,
            IGenaricRepo<DoctorAvailability> availabilityRepo,
            IMapper mapper)
        {
            _bookingRepo = bookingRepo;
            _unitOfWork = unitOfWork;
            this.availabilityRepo = availabilityRepo;
            _mapper = mapper;
        }

        // ================= ADD =================

        //public async Task<bool> Add(AddBookViewModel model)
        //{
        //    if (model == null) return false;

        //    var availability = await availabilityRepo.Get(model.DoctorAvailabilityId);

        //    if (availability == null)
        //        return false;

        //    // Get existing bookings for this availability
        //    var existingBookings = await _bookingRepo.GetAll(
        //        b => b.DoctorAvailabilityId == model.DoctorAvailabilityId);

        //    var count = existingBookings.Count();

        //    // Check max patients
        //    if (count >= availability.MaxPatients)
        //        return false;

        //    // Prevent same patient double booking
        //    if (existingBookings.Any(b => b.PatientId == model.PatientId))
        //        return false;

        //    // Calculate consultation time dynamically
        //    var consultationTime = availability.AvailableFrom
        //        .AddMinutes(count * availability.SessionDurationMinutes);

        //    var booking = new Booking
        //    {
        //        PatientId = model.PatientId,
        //        DoctorAvailabilityId = model.DoctorAvailabilityId,
        //        Status = model.Status,
        //        ConsultionTime = consultationTime
        //    };

        //    _bookingRepo.Add(booking);

        //    return _unitOfWork.SaveChanges() > 0;
        //}



        // ================= DELETE =================


        public async Task<int> Add(AddBookViewModel model)
        {
            if (model == null) return 0;

            var availability = await availabilityRepo.Get(model.DoctorAvailabilityId);
            if (availability == null) return 0;

            var existingBookings = await _bookingRepo.GetAll(
                b => b.DoctorAvailabilityId == model.DoctorAvailabilityId);
            var count = existingBookings.Count();

            if (count >= availability.MaxPatients) return 0;

            if (existingBookings.Any(b => b.PatientId == model.PatientId)) return 0;

            var consultationTime = availability.AvailableFrom
                .AddMinutes(count * availability.SessionDurationMinutes);

            var booking = new Booking
            {
                PatientId = model.PatientId,
                DoctorAvailabilityId = model.DoctorAvailabilityId,
                ConsultionTime = consultationTime,
                Status = AppointmentStatus.Pending,  // ✅ always Pending
            };

            _bookingRepo.Add(booking);
            var saved = _unitOfWork.SaveChanges() > 0;

            return saved ? booking.Id : 0;  // ✅ return the new booking id
        }
        public async Task<bool> Delete(int id)
        {
            if (id <= 0) return false;

            var booking = await _bookingRepo.Get(id);
            if (booking == null) return false;

            _bookingRepo.Delete(booking);

            return _unitOfWork.SaveChanges() > 0;
        }

        // ================= GET ALL =================
        public async Task<GetBookViewModel> GetAll(int availabilityId)
        {
            if (availabilityId <= 0)
                return null;

            var bookings = await _bookingRepo
                .GetAll(b => b.DoctorAvailabilityId == availabilityId);



            if (bookings == null || !bookings.Any())
                return null;
            return _mapper.Map<GetBookViewModel>(bookings);

        }

        // ================= GET BY ID =================
        public async Task<GetBookViewModel?> GetById(int id)
        {
            if (id <= 0) return null;
            var booking = await _bookingRepo.Get(id);
            if (booking == null)
                return null;
            return _mapper.Map<GetBookViewModel>(booking);
        }

        public async Task<List<Booking>> GetBookedPatients(int availabilityId)
        {
            var spec = new BookingSpecification(availabilityId);
            var patients = await _bookingRepo.GetAll(spec);
            var result = new GetBookViewModel
            {
                Id = patients.First().Id, // or any logic
                ConsultationTime = patients.First().ConsultionTime,
                AppointmentDate = patients.First().ConsultionTime.Date,
                End = patients.First().ConsultionTime.AddMinutes(30),

                Status = patients.First().Status.ToString(),

                patients = _mapper.Map<List<PatientViewModel>>(
        patients.Select(b => b.Patient).ToList()
    )
            };
            return patients;


        }

        public async Task AutoCompleteExpiredBookings(int patientId)
        {
            var now = DateTime.UtcNow;

            var spec = new BookingSpecification(b => b.PatientId == patientId &&
                     b.Status == AppointmentStatus.Confirmed);
            var bookings = await _bookingRepo.GetAll(spec);

            foreach (var booking in bookings)
            {
                var sessionEnd = booking.DoctorAvailability.AvailableFrom
                    .AddMinutes(booking.DoctorAvailability.SessionDurationMinutes);

                if (now > sessionEnd)
                {
                    booking.Status = AppointmentStatus.Completed;
                    _bookingRepo.Update(booking);
                }
            }

            _unitOfWork.SaveChanges();
        }

        public async Task<bool> ConfirmBooking(string sessionId)
        {
            try
            {
                // ✅ Ask Stripe if this session was actually paid
                var sessionService = new SessionService();
                var session = await sessionService.GetAsync(sessionId);

                if (session.PaymentStatus != "paid")
                    return false;

                // ✅ Get booking id from Stripe metadata
                if (!session.Metadata.TryGetValue("bookingId", out var bookingIdStr))
                    return false;

                if (!int.TryParse(bookingIdStr, out var bookingId))
                    return false;

                // ✅ Find the booking
                var booking = await _bookingRepo.Get(bookingId);
                if (booking == null) return false;

                // ✅ Only confirm if still Pending
                //if (booking.Status != AppointmentStatus.Pending)
                //    return false;

                // ✅ Update to Confirmed
                booking.Status = AppointmentStatus.Confirmed;
                _bookingRepo.Update(booking);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Booking>> GetAllAppointmentForPatient(int patientId)
        {

            var spec = new PatientAppointmentSpecification(patientId);
            var appointments = await _bookingRepo.GetAll(spec);
            return appointments;
        }
    }
}
