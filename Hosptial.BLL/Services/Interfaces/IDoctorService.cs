using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.DoctorViewModels;
using Hosptial.BLL.ViewModels.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    internal interface IDoctorService
    {
        public Task<bool> Add(AddDoctorViewModel doctor);
        public Task<DoctorViewModel> Get(int id);

  
        public Task<List<DoctorsViewModel>> GetAll(int specialityId);
        public Task<bool> Update(UpdateDoctorViewModel doctor);
        public Task<bool> Delete(int id);

        Task<bool> Register(RegisterDoctorViewModel model);
        Task<bool> Login(LoginViewModel model);

    }
}
