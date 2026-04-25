using AutoMapper;
using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.Specification;
using Hosptial.BLL.ViewModels.BookingViewModels;
using Hosptial.BLL.ViewModels.PatientViewModels;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Interfaces;
using Microsoft.IdentityModel.Tokens.Experimental;
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

        public async Task<bool> Add(AddBookViewModel model)
        {
            if (model == null) return false;

            var availability = await availabilityRepo.Get(model.DoctorAvailabilityId);

            if (availability == null)
                return false;

            // Get existing bookings for this availability
            var existingBookings = await _bookingRepo.GetAll(
                b => b.DoctorAvailabilityId == model.DoctorAvailabilityId);

            var count = existingBookings.Count();

            // Check max patients
            if (count >= availability.MaxPatients)
                return false;

            // Prevent same patient double booking
            if (existingBookings.Any(b => b.PatientId == model.PatientId))
                return false;

            // Calculate consultation time dynamically
            var consultationTime = availability.AvailableFrom
                .AddMinutes(count * availability.SessionDurationMinutes);

            var booking = new Booking
            {
                PatientId = model.PatientId,
                DoctorAvailabilityId = model.DoctorAvailabilityId,
                Status = model.Status,
                ConsultionTime = consultationTime
            };

            _bookingRepo.Add(booking);

            return _unitOfWork.SaveChanges() > 0;
        }

        // ================= DELETE =================
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

        public async Task<List<Booking>> GetAllAppointmentForPatient(int patientId)
        {
            var spec = new PatientAppointmentSpecification(patientId);
            var appointments = await _bookingRepo.GetAll(spec);
            return appointments;
        }
    }
}
