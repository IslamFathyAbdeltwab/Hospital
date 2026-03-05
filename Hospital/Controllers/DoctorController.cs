using Hosptial.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    public class DoctorContoller(IDoctorService doctorService) : Controller
    {
        public JsonResult Index(int doctorId)
        {
           var doctor = doctorService.Get(doctorId);
           return Json(doctor);
        }
    }
}
