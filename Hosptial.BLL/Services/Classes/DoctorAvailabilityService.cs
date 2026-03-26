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
    public class DoctorAvailabilityService(IMapper mapper,IUniteOfWork uniteOfWork ,IBookingService bookingService) : IDoctorAvailabilityService
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
            var bookingPatients =await bookingService.GetAll(Id);
            if(bookingPatients.patients is not null && (bookingPatients.patients.Any()&& bookingPatients.End >DateTime.Now))
            {
                return false;
                //if doctor need to cancel must refund to the patients money for reservation 
            }
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

        public async Task<bool> Update(UpdateDoctorAvailabilityViewModel Update)
        {

            var booking = await bookingService.GetAll(Update.DoctorAvailabilityId);
            if (booking.patients is null || booking.patients.Count != 0) return false;
            var docotravailability = mapper.Map<UpdateDoctorAvailabilityViewModel, DoctorAvailability>(Update);
            uniteOfWork.GetGenaricRepo<DoctorAvailability>().Update(docotravailability);
            return await uniteOfWork.SaveChangesAsync()>0;
                
        }
    }
}
