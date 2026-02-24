using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities
{
    public class Prescription : BaseEntity
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();

    }
    public class Treatment:BaseEntity
    {
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }
        public string MedicationName { get; set; }
        public string Notes { get; set; }
    }
}
