using Hosptial.BLL.ViewModels.BookingViewModels;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.DoctorViewModels;
using Hosptial.BLL.ViewModels.PatientViewModels;
using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task<DoctorViewModel?> Get(int id); 
        public Task<List<DoctorsViewModel>> GetAll(int specialityId);
        public Task<List<DoctorsViewModel>> GetAll();

        public Task<bool> Update(UpdateDoctorViewModel doctor);
        public Task<bool> Delete(int id);

        Task<ValidUserViewModel?> Register(RegisterDoctorViewModel model);
        Task<ValidUserViewModel?> Login(LoginViewModel model);
        public  Task<List<Booking>> GetBookingPatient(int avlId);


        // create prescription for patient

    }
}
