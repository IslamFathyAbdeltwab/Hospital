using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.BookingViewModels;
using Hosptial.BLL.ViewModels.Common;
using Hosptial.BLL.ViewModels.PatientViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/patient")]
    public class PatientController(IPatientService patientService , IBookingService bookingService) : ControllerBase
    {

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
            if (booked)
            {
                return Ok("Appointment booked successfully");
            }
            else
            {
                return BadRequest("Failed to book appointment");
            }
        }
       

      

  

        // logout

        //public async Task<ActionResult> Logout()
        //{
        //    await patientService.l;
        //    return Ok("Logout successful");
        //}



        //
    }
}
