using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities
{
    public class Appointment : BaseEntity
    {
        public string PatientId { get; set; }
        public ApplicationUser Patient { get; set; }

        public string DoctorId { get; set; }
        public ApplicationUser Doctor { get; set; }

        public AppointmentStatus Status { get; set; }

        public Consultation Consultation { get; set; }
    }
    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }
}
