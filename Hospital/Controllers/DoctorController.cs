using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels;
using Hosptial.BLL.ViewModels.DoctorViewModels;
using Hosptital.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/{Contoller}")]
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



        #endregion

        #region Doctor Availbity
        [HttpPost("availability")] // create Availability for doctor
        public async Task<ActionResult> CreateAvailbity(AddDoctorAvailabilityViewModel doctorAvail)
        {
            var createdDoctor = await doctorAvailabilityService.Add(doctorAvail);
            return Ok(createdDoctor);
        }

        // Get Doctor Availabilitie
        [HttpGet("availabilities/{doctorId}")]
        public async Task<ActionResult> GetDoctorAvailabilitie(int doctorId)
        {
            var availabilities = await doctorAvailabilityService.Get(doctorId);
            return Ok(availabilities);
        }

        // Get All Doctor Availabilities
        [HttpGet("availabilities")]
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


        [HttpDelete("availability")]
        public async Task<ActionResult> DeleteAvailbity(int Id)
        {
            var deletedDoctor = await doctorAvailabilityService.Delete(Id);
            return Ok(deletedDoctor);

        } 
        #endregion
    }

}
