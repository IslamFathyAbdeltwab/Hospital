using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.Admin;
using Hosptial.BLL.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        // Mange Admins(superAdmin)
        // get all admin
        [HttpGet]
        [Route("getAllAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await adminService.GetAllAdminsAsync();
            return admins != null ? Ok(admins) : NotFound();
        }

        // add new admin
        [HttpPost]
        [Route("addAdmin")]
        public async Task<IActionResult> AddAdmin( AddAdminViewModel admin)
        {
            var isAdded = await adminService.AddAdminAsync(admin);
            return isAdded ? Ok("Admin added successfully") : BadRequest("Failed to add admin");
        }

        // update admin
        [HttpPut]
        [Route("updateAdmin/{id}")]
        public async Task<IActionResult> UpdateAdmin(int id,  AddAdminViewModel admin)
        {
            var isUpdated = await adminService.UpdateAdminAsync(id, admin);
            return isUpdated ? Ok("Admin updated successfully") : BadRequest("Failed to update admin");
        }

        // delete admin
        [HttpDelete]
        [Route("deleteAdmin/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var isDeleted = await adminService.DeleteAdminAsync(id);
            return isDeleted ? Ok("Admin deleted successfully") : BadRequest("Failed to delete admin");
        }

        // get peending doctors 
        [HttpGet]
        [Route("getPendingDoctors")]
        public async Task<IActionResult> GetPendingDoctors()
        {
            var doctors = await adminService.GetAppendingDoctorsAsync();
            return doctors != null ? Ok(doctors) : NotFound();
        }

        // Approve or reject doctors
        [HttpPost]
        [Route("approveDoctor/{doctorId}")]
        public async Task<IActionResult> ApproveDoctor(int doctorId)
        {
            var isApproved = await adminService.ApproveDoctorAsync(doctorId);
            return isApproved ? Ok("Doctor approved successfully") : BadRequest("Failed to approve doctor");
        }

        // get doctor
        [HttpGet]
        [Route("Doctor/{doctorid}")]
        public async Task<IActionResult> GetDoctor(int doctorid)
        {
            var doct = await adminService.GetDoctorByIdAsync(doctorid);
            if (doct is null)
                return NotFound();
            else return Ok(doct);
            
            
        }


        // login Admin

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            var logined = await adminService.Login(loginView);
            if (logined is null)
            {
                return Unauthorized("Invalid Email or PassWork");
                
            }
            return Ok(logined);
        }






    }
}
