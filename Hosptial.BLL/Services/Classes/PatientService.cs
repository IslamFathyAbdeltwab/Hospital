using AutoMapper;
using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.Specification;
using Hosptial.BLL.ViewModels.Common;
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
    public class PatientService : IPatientService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IUniteOfWork _unitOfWork;
        private readonly IGenaricRepo<Patient> _patientRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PatientService(
            RoleManager<IdentityRole<int>> roleManager,
            IUniteOfWork unitOfWork,
            IGenaricRepo<Patient> patientRepo,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IConfiguration configuration)
        {
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _patientRepo = patientRepo;
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        // ================= ADD / REGISTER =================
        public async Task<ValidUserViewModel?> Register(RegisterPatientViewModel model)
        {
            if (model == null) return null;

            var user = new ApplicationUser
            {
                UserName = model.Name,
                Email = model.Email,
                PhoneNumber = model.Phone
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return null;

            // Assign "Patient" role
            var roleAssigned = await EnsureRoleAndAssign(user, "Patient");
            if (!roleAssigned) return null;

            //if (!await _userManager.IsInRoleAsync(user, "Patient"))
            //    await _userManager.AddToRoleAsync(user, "Patient");

            var patient = _mapper.Map<Patient>(model);
            patient.User = user;

            _patientRepo.Add(patient);
            _unitOfWork.SaveChanges();

            // Build JWT
            var roles = await _userManager.GetRolesAsync(user);
            var token = await GenerateJwtToken(user, roles);

            return new ValidUserViewModel
            {
                Name = user.UserName,
                Email = user.Email,
                Token = token
            };
        }


        // helper method to role
        private async Task<bool> EnsureRoleAndAssign(ApplicationUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole<int>(role));

            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }
        // ================= GET =================
        public async Task<PatientViewModel?> Get(int id)
        {
            if (id <= 0) return null;

            var spec = new PatientSpecification(id);
            var patient = await _patientRepo.Get(spec);
            if (patient == null) return null;

            return _mapper.Map<PatientViewModel>(patient);
        }

        // ================= UPDATE =================
        public async Task<bool> Update(int patientId, UpdatePatientViewModel model)
        {
            if (model == null || patientId <= 0) return false;

            var patient = await _patientRepo.Get(patientId);
            if (patient == null) return false;

            patient.User.UserName = model.Name;
            patient.User.PhoneNumber = model.Phone;

            _patientRepo.Update(patient);
            return _unitOfWork.SaveChanges() > 0;
        }

        // ================= DELETE =================
        public async Task<bool> Delete(int id)
        {
            if (id <= 0) return false;

            var patient = await _patientRepo.Get(id);
            if (patient == null) return false;

            _patientRepo.Delete(patient);
            return _unitOfWork.SaveChanges() > 0;
        }

        // ================= LOGIN =================
        public async Task<ValidUserViewModel?> Login(LoginViewModel model)
        {
            if (model == null) return null;

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return null;

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var token = await GenerateJwtToken(user, roles);

            return new ValidUserViewModel
            {
                Name = user.UserName,
                Email = user.Email,
                Token = token
            };
        }

        // ================= JWT GENERATOR =================
        private async Task<string> GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
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

        public async Task<PatientViewModel?> GetByUserId(int userId)
        {
            if (userId <= 0) return null;

            var spec = new GetPatientByUserIdSpecification(userId);
            var patient = await _patientRepo.Get(spec);
            if (patient == null) return null;

            return _mapper.Map<PatientViewModel>(patient);
        }

        // logout is handled on client side by deleting the token, so no server-side method is needed for that.
    }
}