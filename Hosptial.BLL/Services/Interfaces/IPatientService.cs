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
       public Task<PatientViewModel> Get(int id);
       public Task<bool> Add(AddPatientViewModel patient);
       public Task<bool> Update(UpdatePatientViewModel patient);
       public Task<bool> Delete(int id);

        Task<bool> Register(RegisterPatientViewModel model);
        Task<bool> Login(LoginViewModel model);


    }
}
