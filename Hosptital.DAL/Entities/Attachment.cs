using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities
{
    public class Attachment : BaseEntity
    {

        public int PatientId { get; set; }
        public int AppointmentId { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }

        public string UploadedBy { get; set; } // "Doctor" or "Patient"

        //public DateTime UploadedAt { get; set; }
    }
}
