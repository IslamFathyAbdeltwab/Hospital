using Hosptial.BLL.ViewModels.Admin;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.DoctorViewModels;
using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface IAdminService
    {
        Task<bool> AddAdminAsync(AddAdminViewModel admin);

        Task<ValidUserViewModel?> Login(LoginViewModel model);
        Task<DoctorsViewModel?> GetDoctorByIdAsync(int docId);
        Task<bool> ApproveDoctorAsync(int doctorId);
        Task<bool> DeleteAdminAsync(int id);
        Task<AdminViewModel?> GetAdminByIdAsync(int id);
        Task<List<AdminViewModel?>> GetAllAdminsAsync();
        Task<List<DoctorsViewModel?>> GetAppendingDoctorsAsync();
        Task<bool> UpdateAdminAsync(int id, AddAdminViewModel admin);

    }
}
