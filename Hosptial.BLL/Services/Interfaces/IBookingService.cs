using Hosptial.BLL.ViewModels.BookingViewModels;
using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface IBookingService
    {
        public Task<bool> Add(AddBookViewModel book);
        public Task<bool> Delete(int id);
        public Task<GetBookViewModel> GetAll(int AvailabilityId);
        public Task<GetBookViewModel> GetById(int id);
        public Task<List<Booking>> GetBookedPatients(int availabilityId);
        public Task<List<Booking>> GetAllAppointmentForPatient(int patientId);
    }
}
