using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.PatientViewModels;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Classes
{
    internal class PatientService(IUniteOfWork uniteOfWork) : IPatientService
    {
        public Task<bool> Add(AddPatientViewModel patient)
        {
            throw new NotImplementedException();
        }

        public Task<PatientViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UpdatePatientViewModel patient)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(RegisterPatientViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
