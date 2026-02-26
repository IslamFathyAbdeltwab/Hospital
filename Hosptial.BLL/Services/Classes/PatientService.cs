using AutoMapper;
using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.Common;
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
    internal class PatientService : IPatientService
    {
        private readonly IUniteOfWork _unitOfWork;
        private readonly IGenaricRepo<Patient> _patientRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public PatientService(
            IUniteOfWork unitOfWork,
            IGenaricRepo<Patient> patientRepo,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _patientRepo = patientRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        // ================= ADD =================
        public async Task<bool> Add(AddPatientViewModel model)
        {
            if (model == null) return false;

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return false;

            var patient = _mapper.Map<Patient>(model);
            patient.User = user;

             _patientRepo.Add(patient);
            return  _unitOfWork.SaveChanges() > 0;
        }

        // ================= GET =================
        public async Task<PatientViewModel?> Get(int id)
        {
            if (id <= 0) return null;

            var patient = await _patientRepo.Get(id);
            if (patient == null) return null;

            return _mapper.Map<PatientViewModel>(patient);
        }

        // ================= UPDATE =================
        public async Task<bool> Update(UpdatePatientViewModel model)
        {
            if (model == null || model.Id <= 0) return false;

            var patient = await _patientRepo.Get(model.Id);
            if (patient == null) return false;

            patient.User.UserName = model.Name;
            patient.User.PhoneNumber = model.Phone;

            _patientRepo.Update(patient);
            return  _unitOfWork.SaveChanges() > 0;
        }

        // ================= DELETE =================
        public async Task<bool> Delete(int id)
        {
            if (id <= 0) return false;

            var patient = await _patientRepo.Get(id);
            if (patient == null) return false;

            _patientRepo.Delete(patient);
            return  _unitOfWork.SaveChanges() > 0;
        }

        // ================= REGISTER =================
        public async Task<bool> Register(RegisterPatientViewModel model)
        {
            if (model == null) return false;

            var user = new ApplicationUser
            {
                UserName = model.Name,
                Email = model.Email,
                PhoneNumber = model.Phone
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return false;

            var patient = _mapper.Map<Patient>(model);
            patient.User = user;

             _patientRepo.Add(patient);
            return  _unitOfWork.SaveChanges() > 0;
        }

        // ================= LOGIN =================
        public async Task<bool> Login(LoginViewModel model)
        {
            if (model == null) return false;

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RemeberME,
                false);

            return result.Succeeded;
        }
    }
}
