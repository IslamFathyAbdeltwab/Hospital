using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface IPatientService
    {
       public Task<PatientViewModel?> Get(int id);
       //public Task<bool> Add(AddPatientViewModel patient);
       public Task<bool> Update(UpdatePatientViewModel patient);
       public Task<bool> Delete(int id);

        public Task<PatientViewModel?> GetByUserId(int userId);

        Task<ValidUserViewModel?> Register(RegisterPatientViewModel model);
        Task<ValidUserViewModel?> Login(LoginViewModel model);


    }
}
