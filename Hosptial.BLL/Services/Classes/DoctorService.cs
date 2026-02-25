using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels;
using Hosptial.BLL.ViewModels.DoctorViewModels;
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
    public class DoctorService(IUniteOfWork uniteOfWork,IDoctorRepo doctorRepo) : IDoctorService
    {
        public Task<bool> Add(AddDoctorViewModel doctor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DoctorViewModel> Get(int id)
        {
            //check id valid or not 
          var doctor = doctorRepo.GetIncludeSpecialityAndAppointments(id);
            // map to doctor view model
          throw new NotImplementedException();

        }
        public Task<List<DoctorsViewModel>> GetAll(int specialityId)
        {
            var doctors = doctorRepo.GetIncludeSpeciality(specialityId);
            // map to doctor view model
            throw new NotImplementedException();
        }

        public Task<bool> Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UpdateDoctorViewModel doctor)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDoctorService.Register(RegisterDoctorViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
