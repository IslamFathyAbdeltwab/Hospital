using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.Specification;
using Hosptial.BLL.ViewModels.Admin;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.DoctorViewModels;
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
    public class AdminService : IAdminService
    {
        private readonly IUniteOfWork _uniteOfWork1;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<ApplicationUser> hasher;

        public AdminService(IUniteOfWork uniteOfWork1, UserManager<ApplicationUser> userManager, IConfiguration configuration, IPasswordHasher<ApplicationUser> hasher)
        {
            this._uniteOfWork1 = uniteOfWork1;
            this.userManager = userManager;
            this._configuration = configuration;
            this.hasher = hasher;
        }

        public async Task<bool> AddAdminAsync(AddAdminViewModel dto)
        {
            // 1️⃣ Check if email already exists
            var existingUser = await userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("Email already exists");

            // 2️⃣ Create User
            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Name,
                PhoneNumber = dto.Phone,
                Gender = dto.Gender,



            };

            user.PasswordHash = hasher.HashPassword(user, dto.Password);

            var createResult = await userManager.CreateAsync(user, dto.Password);
            if (!createResult.Succeeded) return false;

            // ⚠️ IMPORTANT: make sure SaveChanges happens here if needed
            // so user.Id is generated

            // 3️⃣ Create Admin entity
            var admin = new Admin
            {
                UserId = user.Id,
                Name = user.UserName,
                User = user



            };

            _uniteOfWork1.GetGenaricRepo<Admin>().Add(admin);
            return _uniteOfWork1.SaveChanges() > 0;
        }

        public async Task<bool> DeleteAdminAsync(int id)
        {
            var admin = await _uniteOfWork1.GetGenaricRepo<Admin>().Get(id);
            _uniteOfWork1.GetGenaricRepo<Admin>().Delete(admin);
            return _uniteOfWork1.SaveChanges() > 0;
        }

        public async Task<AdminViewModel?> GetAdminByIdAsync(int id)
        {
            var spec = new AdminWithUserSpecification(id);
            var admin = await _uniteOfWork1.GetGenaricRepo<Admin>().Get(spec);
            if (admin == null) return null;
            var adminViewModel = new AdminViewModel
            {
                Id = admin.Id,
                Name = admin.Name,
                Gender = admin.User.Gender,
                Phone = admin.User.PhoneNumber





            };
            return adminViewModel;
        }

        public async Task<List<AdminViewModel?>> GetAllAdminsAsync()
        {
            var spec = new AdminWithUserSpecification();
            var admins = await _uniteOfWork1.GetGenaricRepo<Admin>().GetAll(spec);
            var adminViewModels = admins.Select(a => new AdminViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Email = a.User.Email,
                Gender = a.User.Gender,
                Phone = a.User.PhoneNumber

            }).ToList();
            return adminViewModels;
        }

        public async Task<List<DoctorsViewModel?>> GetAppendingDoctorsAsync()
        {
            var spec = new DoctorGetAllSpecification(d => d.IsApproved == false);
            var doctorsPendding = await _uniteOfWork1.GetGenaricRepo<Doctor>().GetAll(spec);
            var doctorViewModels = doctorsPendding.Select(d => new DoctorsViewModel
            {
                Id = d.Id,
                Name = d.User.UserName,
                Bio = d.Bio,
                IsApproed = d.IsApproved,

                Gender = d.User.Gender,
                Phone = d.User.PhoneNumber,
                YearsOfExperience = d.YearsOfExperienc,


                Speciality = d.Speciality.Name,

            }).ToList();

            return doctorViewModels;
        }

        public async Task<bool> ApproveDoctorAsync(int doctorId)
        {
            var doc = await _uniteOfWork1.GetGenaricRepo<Doctor>().Get(doctorId);
            if (doc == null) return false;
            doc.IsApproved = true;
            _uniteOfWork1.GetGenaricRepo<Doctor>().Update(doc);
            return _uniteOfWork1.SaveChanges() > 0;
        }

        public async Task<bool> UpdateAdminAsync(int id, AddAdminViewModel admin)
        {
            var spec = new AdminWithUserSpecification(id);
            var adminToUpdate = await _uniteOfWork1.GetGenaricRepo<Admin>().Get(spec);
            if (adminToUpdate == null) return false;
            adminToUpdate.Name = admin.Name;
            adminToUpdate.User.Email = admin.Email;
            adminToUpdate.User.PasswordHash = admin.Password;
            _uniteOfWork1.GetGenaricRepo<Admin>().Update(adminToUpdate);
            return _uniteOfWork1.SaveChanges() > 0;



        }

        public async Task<DoctorsViewModel?> GetDoctorByIdAsync(int docId)
        {
            var spec = new DoctorSpecification(docId);
            var doc = await _uniteOfWork1.GetGenaricRepo<Doctor>().Get(spec);
            if (doc == null) return null;
            var docViewMOdel = new DoctorsViewModel()
            {
                Id = docId,
                IsApproed = doc.IsApproved,

                Bio = doc.Bio,
                Gender = doc.User.Gender,
                Name = doc.User.UserName,
                Phone = doc.User.PhoneNumber,
                Speciality = doc.Speciality.Name,
                YearsOfExperience = doc.YearsOfExperienc

            };
            return docViewMOdel;
        }




        public async Task<ValidUserViewModel?> Login(LoginViewModel model)
        {
            if (model == null) return null;

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) return null;

            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid) return null;

            var roles = await userManager.GetRolesAsync(user);
            var token = await GenerateJwtToken(user, roles);

            return new ValidUserViewModel
            {
                Name = user.UserName,
                Email = user.Email,
                Token = token
            };
        }
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
    }
}
