using AutoMapper;
using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Classes
{
    public class DoctorAvailabilityService(IMapper mapper,IUniteOfWork uniteOfWork) : IDoctorAvailabilityService
    {
        public async Task<bool> Add(AddDoctorAvailabilityViewModel doctorAvailability)
        {
            var doctorAvailabilityEntity = mapper.Map<DoctorAvailability>(doctorAvailability);
            uniteOfWork.GetGenaricRepo<DoctorAvailability>().Add(doctorAvailabilityEntity);
            return await uniteOfWork.SaveChangesAsync()>0;
        }

        public async Task<bool> Delete(int Id)
        {
            var doctorAvailability = await uniteOfWork.GetGenaricRepo<DoctorAvailability>().Get(Id);
            if(doctorAvailability is  null)
                return false;
            uniteOfWork.GetGenaricRepo<DoctorAvailability>().Delete(doctorAvailability);
            return await uniteOfWork.SaveChangesAsync() > 0;
        }

        public async Task<DoctorAvailabilityViewModel> Get(int Id)
        {
           var doctorAvailability = await uniteOfWork.GetGenaricRepo<DoctorAvailability>().Get(Id);
            if (doctorAvailability is null)
                return null;
            var doctorAvailabilityViewModel = mapper.Map<DoctorAvailabilityViewModel>(doctorAvailability);
            // must calc the book complete properties from booking service ;
            return doctorAvailabilityViewModel;
        }

        public async Task<List<DoctorAvailabilityViewModel>> GetAll(int doctorId)
        {
            var doctrAvas =await uniteOfWork.GetGenaricRepo<DoctorAvailability>().GetAll(x => x.DoctorId == doctorId);
            if(doctrAvas is null)
                return null;
          return mapper.Map<List<DoctorAvailabilityViewModel>>(doctrAvas);

        }

        public Task<bool> Update(UpdateDoctorAvailabilityViewModel Update)
        {
            throw new NotImplementedException();
        }
    }
}
