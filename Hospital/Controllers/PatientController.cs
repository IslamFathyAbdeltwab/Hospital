using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Hosptial.BLL.ViewModels.BookingViewModels;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.PatientViewModels;
using Hosptital.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        //[HttpPost("Book")]
        //public async Task<ActionResult> GreateBook(AddBookViewModel appointment)
        //{
        //    var booked = await bookingService.Add(appointment);

        //    if (!booked)
        //        return BadRequest("Failed to book appointment");

        //    var url = await paymentService.CreateCheckout(
        //        new PaymentDto { Amount = appointment.Amount }
        //    );

        //    return Ok(new { stripeUrl = url });
        //}

        [HttpPost("Book")]
        public async Task<ActionResult> CreateBook(AddBookViewModel appointment)
        {
            // ✅ Create booking — get back the new id
            var bookingId = await bookingService.Add(appointment);

            if (bookingId == 0)
                return BadRequest("Failed to book appointment.");

            // ✅ Pass booking id + amount to Stripe
            var url = await paymentService.CreateCheckout(new PaymentDto
            {
                Amount = (long)(appointment.Amount * 100),  // Stripe needs cents
                BookingId = bookingId
            });

            return Ok(new { stripeUrl = url });
        }


        [HttpPost("confirm")]
        public async Task<ActionResult> ConfirmBooking([FromQuery] string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                return BadRequest("Session ID is required.");

            var result = await bookingService.ConfirmBooking(sessionId);

            if (!result)
                return BadRequest("Could not confirm payment.");

            return Ok(new { message = "Appointment confirmed successfully." });
        }


        [HttpGet("Appointments/{patientId}")]
        public async Task<ActionResult> GetAppointments(int patientId)
        {
            await bookingService.AutoCompleteExpiredBookings(patientId);
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
            if (!updated)
            {
                return NotFound("Patient not found");
            }
            return Ok(updated);
        }

        //[HttpPost("confirm")]
        //public async Task<IActionResult> ConfirmBooking([FromQuery] string sessionId)
        //{
        //    // 1. Ask Stripe if this session was paid
        //    var service = new SessionService();
        //    var session = await service.GetAsync(sessionId);

        //    if (session.PaymentStatus != "paid")
        //        return BadRequest("Payment not completed.");

        //    // 2. Find the booking by the session metadata you stored at creation
        //    // (store appointmentId in Stripe metadata when creating the session)
        //    var appointmentId = int.Parse(session.Metadata["appointmentId"]);
        //    var appointment = await _context.Appointments.FindAsync(appointmentId);

        //    if (appointment == null) return NotFound();

        //    // 3. Update status
        //    appointment.Status = AppointmentStatus.Confirmed;
        //    await _context.SaveChangesAsync();

        //    return Ok(appointment);
        //}

        //
    }
}
