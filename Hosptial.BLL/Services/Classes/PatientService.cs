using Hosptial.BLL.Services.Interfaces;
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
        public Task Register(RegisterPatientViewModel patient)
        {
            throw new NotImplementedException();
        }
    }
}
