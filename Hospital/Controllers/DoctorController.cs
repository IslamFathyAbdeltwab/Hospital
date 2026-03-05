using Hosptial.BLL.Services.Interfaces;
using Hosptital.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/{Contoller}")]
    public class DoctorContoller(IDoctorService doctorService) : ControllerBase
    {
        [HttpGet("{doctorId}")]
        public async Task<ActionResult> Index(int doctorId)
        {
            var doctor = await doctorService.Get(doctorId);
            return Ok(doctor);
        }
    }
}
