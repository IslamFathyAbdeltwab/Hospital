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
        public Task Register(RegisterPatientViewModel patient);
    }
}
