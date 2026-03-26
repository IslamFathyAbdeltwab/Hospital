using Hosptital.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface IAttachmentService
    {
        Task<Attachment> UploadAsync(IFormFile file, int patientId, int appointmentId, string uploadedBy);
        Task<List<Attachment>> GetByAppointmentAsync(int appointmentId);
        Task<(byte[] fileBytes, string contentType, string fileName)> DownloadAsync(int id);
    }
}
