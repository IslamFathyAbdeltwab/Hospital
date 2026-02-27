using AutoMapper;
using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.BookingViewModels;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<GetBookViewModel>> GetAll(int availabilityId)
        {
            if (availabilityId <= 0)
                return new List<GetBookViewModel>();

            var bookings = await _bookingRepo
                .GetAll(b => b.DoctorAvailabilityId == availabilityId);



            if (bookings == null || !bookings.Any())
                return new List<GetBookViewModel>();

            return _mapper.Map<List<GetBookViewModel>>(bookings);
        }

        // ================= GET BY ID =================
        public async Task<GetBookViewModel?> GetById(int id)
        {
            if (id <= 0) return null;
            var booking = await _bookingRepo
                .Get(id);
            if (booking == null)
                return null;
            return _mapper.Map<GetBookViewModel>(booking);
        }
    }
}
