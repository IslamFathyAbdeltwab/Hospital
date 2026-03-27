using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels;
using Hosptial.BLL.ViewModels.DoctorViewModels;
using Hosptial.BLL.ViewModels.PrescriptionViewModels;
using Hosptital.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/Doctor")]
    public class DoctorContoller(IDoctorService doctorService, IPrescriptionService prescriptionService, IDoctorAvailabilityService doctorAvailabilityService) : ControllerBase
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
            if (entered != null)
            {
                return Ok(entered);
            }
            else
            {
                return Unauthorized("Invalid email or password");
            }
        }

        // register
        [HttpPost("Register")] // register doctor
        public async Task<ActionResult> Register(RegisterDoctorViewModel doctor)
        {
            var createdDoctor = await doctorService.Register(doctor);
            if (createdDoctor != null)
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

        [HttpGet("BookingPatients/{avlId}")]
        public async Task<ActionResult> GetBookingPatient(int avlId)
        {
            var patients = await doctorService.GetBookingPatient(avlId);
            return Ok(patients);
        }

        // create prescription for patient
        [HttpPost("Prescription")]
        public async Task<ActionResult> CreatePrescription(AddPrescriptionViewModel addPrescription)
        {
            var created = await prescriptionService.Add(addPrescription);
            if (created)
                return Ok("Created Successfuly");
            else
                return BadRequest();
        }
            

        // get specific prescription
        [HttpGet("Prescription/{presId}")]
        public async Task<ActionResult> GetPrscription(int presId)
        {
            var prescription = await prescriptionService.Get(presId);
            return Ok(prescription);
        }

        // get all prescriptions
        [HttpGet("Prescriptions/{patientId}")]
        public async Task<ActionResult> GetAllPrescriptions(int patientId)
        {
            var pres = await prescriptionService.GetAll(patientId);
            return Ok(pres);

        }





    }
}
