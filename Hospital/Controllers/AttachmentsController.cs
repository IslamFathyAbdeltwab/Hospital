using Hosptial.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentsController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            IFormFile? file  ,
           [FromForm] int patientId,
        [FromForm] int appointmentId,
          [FromForm] string? uploadedBy)
        {
            try
            {
                var result = await _attachmentService.UploadAsync(file, patientId, appointmentId, uploadedBy);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointment(int appointmentId)
        {
            var files = await _attachmentService.GetByAppointmentAsync(appointmentId);
            return Ok(files);
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var (bytes, contentType, fileName) = await _attachmentService.DownloadAsync(id);
                return File(bytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
