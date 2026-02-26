using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.BookingViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Classes
{
    public class BookingService : IBookingService
    {
        public Task<bool> Add(AddBookViewModel book)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetBookViewModel>> GetAll(int AvailabilityId)
        {
            throw new NotImplementedException();
        }

        public Task<GetBookViewModel> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
