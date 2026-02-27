using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Classes
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        public Task<bool> Add(AddDoctorAvailabilityViewModel doctorAvailability)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<DoctorAvailabilityViewModel> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DoctorAvailabilityViewModel>> GetAll(int doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UpdateDoctorAvailabilityViewModel Update)
        {
            throw new NotImplementedException();
        }
    }
}
