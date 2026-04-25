using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Hosptial.BLL.ViewModels.BookingViewModels;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.PatientViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/patient")]
    public class PatientController(IPatientService patientService ,IDoctorService doctorService ,IBookingService bookingService,IPaymentService paymentService) : ControllerBase
    {

        // get patient by userID
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetPatientByUserId(int userId)
        {
            var patient = await patientService.GetByUserId(userId);
            if (patient is null)
            {
                return NotFound("Patient not found");
            }
            return Ok(patient);
        }
        // login 
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginViewModel loginView)
        {
            var entered = await patientService.Login(loginView);
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
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterPatientViewModel registerView)
        {
            //check the modelstate
            var registered = await patientService.Register(registerView);
            if (registered != null)
            {
                return Ok(registered);
            }
            else
            {
                return BadRequest("Registration failed");
            }
        }
        // profile  
        [HttpGet("Profile/{patientId}")]
        public async Task<ActionResult> Index(int patientId)
        {
            var patient = await patientService.Get(patientId);
            return Ok(patient);

        }
        // book availaib
        [HttpPost("Book")]
        public async Task<ActionResult> GreateBook(AddBookViewModel appointment)
        {
            var booked = await bookingService.Add(appointment);

            if (!booked)
                return BadRequest("Failed to book appointment");

            var url = await paymentService.CreateCheckout(
                new PaymentDto { Amount = appointment.Amount }
            );

            return Ok(new { stripeUrl = url });
        }


        [HttpGet("Appointments/{patientId}")]
        public async Task<ActionResult> GetAppointments(int patientId)
        {
            var appointments = await bookingService.GetAllAppointmentForPatient(patientId);
            return Ok(appointments);
        }

        // get all doctors 
        [HttpGet("Doctors")]
        public async Task<ActionResult> GetAllDoctors()
        {
            var doctors = await doctorService.GetAll();
            if (doctors is null)
            {
                return NotFound("No doctors found");
            }

            return Ok(doctors);
        }

        // get doctor by speciality
        [HttpGet("Doctors/{specialityId}")]
        public async Task<ActionResult> GetDoctorsBySpeciality(int specialityId)
        {
            var doctors = await doctorService.GetAll(specialityId);
            if (doctors is null || doctors.Count == 0)
            {
                return NotFound("No doctors found for the specified speciality");
            }
            return Ok(doctors);
        }

        // get doctor by id
        [HttpGet("Doctor/{doctorId}")]
        public async Task<ActionResult> GetDoctorById(int doctorId)
        {
            var doctor = await doctorService.Get(doctorId);
            if (doctor is null)
            {
                return NotFound("Doctor not found");
            }
            return Ok(doctor);
        }

        // logout

        //public async Task<ActionResult> Logout()
        //{
        //    await patientService.l;
        //    return Ok("Logout successful");
        //}


        // updata profiel
        [HttpPut("UpdateProfile/{patientId}")]
        public async Task<ActionResult> UpdateProfile(int patientId, UpdatePatientViewModel updateView)
        {
            var updated = await patientService.Update(patientId, updateView);
            if (updated)
            {
                return NotFound("Patient not found");
            }
            return Ok(updated);
        }


        //
    }
}
