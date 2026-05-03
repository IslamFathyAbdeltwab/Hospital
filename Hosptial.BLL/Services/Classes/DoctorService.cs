using AutoMapper;
using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.Specification;
using Hosptial.BLL.ViewModels.BookingViewModels;
using Hosptial.BLL.ViewModels.Common;
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
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Classes
{
    public class DoctorService : IDoctorService
    {
        private readonly IConfiguration _configuration;
        private readonly IUniteOfWork _unitOfWork;
        private readonly IDoctorRepo _doctorRepo;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public DoctorService(
            IConfiguration configuration,
            IUniteOfWork unitOfWork,
            IDoctorRepo doctorRepo,
            RoleManager<IdentityRole<int>> roleManager,
            UserManager<ApplicationUser> userManager,
            IBookingService bookingService,
            IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _doctorRepo = doctorRepo;
            _roleManager = roleManager;
            _userManager = userManager;
            _bookingService = bookingService;
            _mapper = mapper;
        }

        #region CRUD

        public async Task<bool> Delete(int id)
        {
            if (id <= 0) return false;

            var doctor = await _doctorRepo.Get(id);
            if (doctor == null) return false;

            _doctorRepo.Delete(doctor);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<DoctorViewModel?> Get(int id)
        {
            if (id <= 0) return null;

            var spec = new DoctorSpecification(id);
            var doctor = await _doctorRepo.Get(spec); // Include speciality & availabilities
            if (doctor == null) return null;

            return _mapper.Map<DoctorViewModel>(doctor);
        }

        public async Task<List<DoctorsViewModel>> GetAll(int specialityId)
        {
            var spec = new DoctorGetAllSpecification(specialityId);
            var doctors = await _doctorRepo.GetAll(spec);

            return doctors?.Any() == true
                ? _mapper.Map<List<DoctorsViewModel>>(doctors)
                : new List<DoctorsViewModel>();
        }

        public async Task<List<DoctorsViewModel>> GetAll()
        {
            var spec = new DoctorGetAllSpecification();
            var doctors = await _doctorRepo.GetAll(spec);

            return doctors?.Any() == true
                ? _mapper.Map<List<DoctorsViewModel>>(doctors)
                : new List<DoctorsViewModel>();
        }

        public async Task<bool> Update(UpdateDoctorViewModel model)
        {
            if (model == null || model.Id <= 0) return false;

            var doctor = await _doctorRepo.Get(model.Id);
            if (doctor == null) return false;

            doctor.Bio = model.Bio;
            doctor.YearsOfExperienc = model.YearsOfExperienc;
            doctor.SpecialityId = model.SpecialityId;

            _doctorRepo.Update(doctor);
            return _unitOfWork.SaveChanges() > 0;
        }

        #endregion

        #region Authentication / JWT

        public async Task<ValidUserViewModel?> Login(LoginViewModel model)
        {
            if (model == null) return null;
            

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return null;

            var spec = new DoctorSpecification(d => d.UserId == user.Id);
           var doc = await _unitOfWork.GetGenaricRepo<Doctor>().Get(spec);
            if (doc == null) return null;
            if (!doc.IsApproved) return null;

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid) return null;

            return await BuildValidUserViewModel(user,doc.Id);
        }

        public async Task<ValidUserViewModel?> Register(RegisterDoctorViewModel model)
        {
            if (model == null) return null;

            var doctor = _mapper.Map<Doctor>(model);
            var user = new ApplicationUser
            {
                UserName = model.Name,
                Email = model.Email,
                Gender = model.Gender,
                PhoneNumber = model.Phone
            };


            var createResult = await _userManager.CreateAsync(user, model.Password);
            if (!createResult.Succeeded) return null;

            var roleAssigned = await EnsureRoleAndAssign(user, "Doctor");
            if (!roleAssigned) return null;

            doctor.User = user;
            doctor.IsApproved = false;
            _doctorRepo.Add(doctor);
            _unitOfWork.SaveChanges();

            return await BuildValidUserViewModel(user,doctor.Id);
        }

        private async Task<bool> EnsureRoleAndAssign(ApplicationUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole<int>(role));

            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        private async Task<ValidUserViewModel> BuildValidUserViewModel(ApplicationUser user ,int DoctorId)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = await GenerateJwtToken(user, roles,DoctorId);

            return new ValidUserViewModel
            {
                Name = user.UserName,
                Email = user.Email,
                Token = token
            };
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user, IList<string> roles,int doctorId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("DoctorId",doctorId.ToString())
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: _configuration["jwt:Issuer"],
                audience: _configuration["jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

        public async Task<List<Booking>> GetBookingPatient(int avlId)
        {
            if (avlId <= 0) return null;
            return await _bookingService.GetBookedPatients(avlId);
        }
        
    }
}