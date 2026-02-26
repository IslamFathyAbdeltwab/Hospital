using Hosptial.BLL.ViewModels.BookingViewModels;
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
        public Task<List<GetBookViewModel>> GetAll(int AvailabilityId);
        public Task<GetBookViewModel> GetById(int id);
    }
}
