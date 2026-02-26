using AutoMapper;
using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels;
using Hosptial.BLL.ViewModels.DoctorViewModels;
using Hosptial.BLL.ViewModels.PatientViewModels;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Entities.Base;
using Hosptital.DAL.Repositroyes.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Classes
{
    public class DoctorService : IDoctorService
    {
        private readonly IUniteOfWork _unitOfWork;
        private readonly IDoctorRepo _doctorRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public DoctorService(
            IUniteOfWork unitOfWork,
            IDoctorRepo doctorRepo,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _doctorRepo = doctorRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<bool> Add(AddDoctorViewModel model)
        {
            if (model is null) return false;

            var doctor = _mapper.Map<Doctor>(model);

            var user = new ApplicationUser
            {
                UserName = model.Name,
                Email = model.Email,
                Gender = model.Gender
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return false;

            doctor.User = user;

             _doctorRepo.Add(doctor);
            return  _unitOfWork.SaveChanges() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0) return false;

            var doctor = await _doctorRepo.Get(id);
            if (doctor is null) return false;

            _doctorRepo.Delete(doctor);
            return  _unitOfWork.SaveChanges() > 0;
        }

        public async Task<DoctorViewModel?> Get(int id)
        {
            if (id <= 0) return null;

            var doctor = await _doctorRepo.GetIncludeSpecialityAndAppointments(id);
            if (doctor is null) return null;

            var viewModel = _mapper.Map<DoctorViewModel>(doctor);

            viewModel.DoctorAvailabilites = doctor.DoctorAvailabilities?
                .Select(a => new DoctorAvailabilityViewModel
                {
                    doctorAvailability = a
                }).ToList();

            return viewModel;
        }

        public async Task<List<DoctorsViewModel>> GetAll(int specialityId)
        {
            var doctors = await _doctorRepo.GetIncludeSpeciality(specialityId);

            if (doctors == null || !doctors.Any())
                return new List<DoctorsViewModel>();

            return _mapper.Map<List<DoctorsViewModel>>(doctors);
        }

        public async Task<bool> Update(UpdateDoctorViewModel model)
        {
            if (model is null || model.Id <= 0) return false;

            var doctor = await _doctorRepo.Get(model.Id);
            if (doctor is null) return false;

            doctor.User.UserName = model.Name;
            doctor.Bio = model.Bio;
            doctor.YearsOfExperienc = model.YearsOfExperienc;
            doctor.SpecialityId = model.SpecialityId;

            _doctorRepo.Update(doctor);
            return  _unitOfWork.SaveChanges() > 0;
        }

        public async Task<bool> Login(LoginViewModel model)
        {
            if (model is null) return false;

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RemeberME,
                lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task<bool> Register(RegisterDoctorViewModel model)
        {
            if (model is null) return false;

            var doctor = _mapper.Map<Doctor>(model);

            var user = new ApplicationUser
            {
                UserName = model.Name,
                Email = model.Email,
                Gender = model.Gender,
                PhoneNumber = model.Phone
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return false;

            doctor.User = user;

             _doctorRepo.Add(doctor);
            return  _unitOfWork.SaveChanges() > 0;
        }
    }

}
