using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels;
using Hosptial.BLL.ViewModels.DoctorViewModels;
using Hosptital.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/Doctor")]
    public class DoctorContoller(IDoctorService doctorService, IDoctorAvailabilityService doctorAvailabilityService) : ControllerBase
    {

        #region Profile
        // get profile
        [HttpGet("Profile/{doctorId}")]
        public async Task<ActionResult> Index(int doctorId)
        {
            var doctor = await doctorService.Get(doctorId);
            return Ok(doctor);
        }

        // Update profile
        [HttpPut("Profile")] // update doctor profile
        public async Task<ActionResult> UpdateDoctor(UpdateDoctorViewModel doctor)
        {
            var updatedDoctor = await doctorService.Update(doctor);
            return Ok(updatedDoctor);
        } 


        // login 
        [HttpPost("Login")] // login doctor
        public async Task<ActionResult> Login(LoginViewModel loginView)
        {
            var entered = await doctorService.Login(loginView);
            if (entered)
            {
                return Ok("Login successful");
            }
            else
            {
                return Unauthorized("Invalid email or password");
            }
        }

        // register
        [HttpPost("Register")] // register doctor
        public async Task<ActionResult> Register(AddDoctorViewModel doctor)
        {
            var createdDoctor = await doctorService.Add(doctor);
            if (createdDoctor)
            {
                return Ok(createdDoctor);
            }
            else
            {
                return BadRequest("Failed to create doctor profile");
            }
        }



        #endregion

        #region Doctor Availbity
        [HttpPost("availability")] // create Availability for doctor
        public async Task<ActionResult> CreateAvailbity(AddDoctorAvailabilityViewModel doctorAvail)
        {
            var createdDoctor = await doctorAvailabilityService.Add(doctorAvail);
            return Ok(createdDoctor);
        }

        // Get Doctor Availabilitie
        [HttpGet("availabilitie/{id}")]
        public async Task<ActionResult> GetAvailabilitie(int id)
        {
            var availabilities = await doctorAvailabilityService.Get(id);
            return Ok(availabilities);
        }

        // Get All Doctor Availabilities
        [HttpGet("availabilities/{doctorId}")]
        public async Task<ActionResult> GetAllDoctorAvailabilities(int doctorId)
        {
            var availabilities = await doctorAvailabilityService.GetAll(doctorId);
            return Ok(availabilities);
        }

        // Update Doctor Availabilities
        [HttpPut("availabilities")]
        public async Task<ActionResult> UpdateAvailbity(UpdateDoctorAvailabilityViewModel doctorAvail)
        {
            var updatedDoctor = await doctorAvailabilityService.Update(doctorAvail);
            return Ok(updatedDoctor);
        }


        [HttpDelete("availability/{Id}")]
        public async Task<ActionResult> DeleteAvailbity(int Id)
        {
            var deletedDoctor = await doctorAvailabilityService.Delete(Id);
            return Ok(deletedDoctor);

        } 
        #endregion
    }

}
