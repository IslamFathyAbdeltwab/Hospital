using Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface IDoctorAvailabilityService
    {
        public Task<bool> Add(AddDoctorAvailabilityViewModel doctorAvailability);

        public Task<bool> Delete(int Id);

        public Task<DoctorAvailabilityViewModel> Get(int Id);

        public Task<List<DoctorAvailabilityViewModel>> GetAll(int doctorId);

        public Task<bool> Update(UpdateDoctorAvailabilityViewModel Update);
    }
}
