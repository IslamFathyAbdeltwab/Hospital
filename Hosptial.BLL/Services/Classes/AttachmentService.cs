using Hosptial.BLL.Services.Interfaces;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Classes
{
    public class AttachmentService : IAttachmentService
    {
        //private readonly IWebHostEnvironment _env;
        private readonly IUniteOfWork uniteOfWork;

        public AttachmentService(IUniteOfWork uniteOfWork)
        {
            //_env = env;
            this.uniteOfWork = uniteOfWork;
        }

        public async Task<Attachment> UploadAsync(IFormFile file, int patientId, int appointmentId, string uploadedBy)
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded");

            //var allowedTypes = new[] { "application/pdf", "image/jpeg", "image/png" };
            //if (!allowedTypes.Contains(file.ContentType))
            //    throw new Exception("Invalid file type");

            if (file.Length > 5 * 1024 * 1024)
                throw new Exception("File too large");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var attachment = new Attachment
            {
                PatientId = patientId,
                AppointmentId = appointmentId,
                FileName = file.FileName,
                FilePath = "/uploads/" + uniqueFileName,
                ContentType = file.ContentType,
                UploadedBy = uploadedBy,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
                
            };

            uniteOfWork.GetGenaricRepo<Attachment>().Add(attachment);
            await uniteOfWork.SaveChangesAsync();

            return attachment;
        }

        public async Task<List<Attachment>> GetByAppointmentAsync(int appointmentId)
        {
            var res = await uniteOfWork.GetGenaricRepo<Attachment>().GetAll(a => a.AppointmentId == appointmentId);

            return res.OrderByDescending(a => a.UpdatedAt).ToList();
        }

        public async Task<(byte[] fileBytes, string contentType, string fileName)> DownloadAsync(int id)
        {
            var file = await uniteOfWork.GetGenaricRepo<Attachment>().Get(id);

            if (file == null)
                throw new Exception("File not found");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FilePath.TrimStart('/')); 

            var bytes = await System.IO.File.ReadAllBytesAsync(path);

            return (bytes, file.ContentType, file.FileName);
        }

    }
    }
