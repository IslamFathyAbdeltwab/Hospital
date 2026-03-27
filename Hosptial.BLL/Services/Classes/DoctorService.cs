using AutoMapper;
using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.Specification;
using Hosptial.BLL.ViewModels.BookingViewModels;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels;
using Hosptial.BLL.ViewModels.DoctorViewModels;
using Hosptial.BLL.ViewModels.PatientViewModels;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Entities.Base;
using Hosptital.DAL.Repositroyes.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Classes
{
    public class DoctorService : IDoctorService
    {
        private readonly IConfiguration configuration;
        private readonly IUniteOfWork _unitOfWork;
        private readonly IDoctorRepo _doctorRepo;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBookingService bookingService;
        private readonly IMapper _mapper;

        public DoctorService(
            IConfiguration configuration,
            IUniteOfWork unitOfWork,
            IDoctorRepo doctorRepo,
            RoleManager<IdentityRole<int>> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IBookingService bookingService,


            IMapper mapper)
        {
            this.configuration = configuration;
            _unitOfWork = unitOfWork;
            _doctorRepo = doctorRepo;
            this.roleManager = roleManager;
            this.roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            this.bookingService = bookingService;
            _mapper = mapper;
        }


        public async Task<bool> Delete(int id)
        {
            if (id <= 0) return false;

            var doctor = await _doctorRepo.Get(id);
            if (doctor is null) return false;

            _doctorRepo.Delete(doctor);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<DoctorViewModel?> Get(int id)
        {
            if (id <= 0) return null;

            var spec = new DoctorSpecification(id);
            var doctor = await _doctorRepo.Get(spec);        //need to include speciality and doctor availabilities
            if (doctor is null) return null;


            var viewModel = _mapper.Map<DoctorViewModel>(doctor);


            return viewModel;
        }

        public async Task<List<DoctorsViewModel>> GetAll(int specialityId)
        {
            var spec = new DoctorGetAllSpecification(specialityId);
            var doctors = await _doctorRepo.GetAll(spec);
            if (doctors == null || !doctors.Any())
                return new List<DoctorsViewModel>();

            return _mapper.Map<List<DoctorsViewModel>>(doctors);
        }

        public async Task<bool> Update(UpdateDoctorViewModel model)
        {
            if (model is null || model.Id <= 0) return false;

            var doctor = await _doctorRepo.Get(model.Id);
            if (doctor is null) return false;

            //doctor.User.UserName = model.Name; // here dont't need to updata the docotr name
            doctor.Bio = model.Bio;
            doctor.YearsOfExperienc = model.YearsOfExperienc;
            doctor.SpecialityId = model.SpecialityId;

            _doctorRepo.Update(doctor);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<ValidUserViewModel?> Login(LoginViewModel model)
        {
            if (model == null) return null;

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                model.Password,
                model.RemeberME,
                lockoutOnFailure: false);
            if (result.Succeeded)
                return await BuildValidUserViewModel(user, "Doctor");
            return null;
        }
        public async Task<ValidUserViewModel?> Register(RegisterDoctorViewModel model)
        {
            if (model == null)
                return null;
            var doctor = _mapper.Map<Doctor>(model);

            var user = CreateUser(model);

            try
            {
                var createResult = await _userManager.CreateAsync(user, model.Password);
                if (!createResult.Succeeded)
                    return null;

                var roleCreateAndAssigned = CreateAndAssignRole(user, "Doctor");
                if (!await roleCreateAndAssigned) return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            doctor.User = user;

            _doctorRepo.Add(doctor);
            _unitOfWork.SaveChanges();

            return await BuildValidUserViewModel(user, "Doctor");
        }

        private async Task<bool> CreateAndAssignRole(ApplicationUser user, string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(role));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, role);

            if (!roleResult.Succeeded)
                return false;
            return true;
        }

        private ApplicationUser CreateUser(RegisterDoctorViewModel model)
        {
            return new ApplicationUser
            {
                UserName = model.Name,
                Email = model.Email,
                Gender = model.Gender,
                PhoneNumber = model.Phone,
            };
        }

        private async Task<ValidUserViewModel> BuildValidUserViewModel(ApplicationUser user, string role)
        {
            return new ValidUserViewModel
            {
                Name = user.UserName,
                Email = user.Email,
                Token = await GenerateJwtToken(user)
            };
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user )
        {
            var clims = new List<System.Security.Claims.Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Doctor")
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                clims.Add(new Claim(ClaimTypes.Role, role));
            }
            var issuer = configuration["jwt:issuer"]; // Replace with your actual issuer
            var audience = configuration["jwt:audience"]; // Replace with your actual audience
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]!)); // Replace with your actual secret key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: clims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<GetBookViewModel> GetBookingPatient(int avlId)
        {
            if (avlId <= 0) return null;
            var patient = await bookingService.GetBookedPatients(avlId);
            return patient;
        }
    }
}
//"islam": hint the register like add functionalty so delete one 