using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities
{
    public class Booking : BaseEntity
    {
        // Patient
        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        // Booked Slot
        public int DoctorAvailabilityId { get; set; }
        public DoctorAvailability DoctorAvailability { get; set; }

        public AppointmentStatus Status { get; set; }

        public DateTime ConsultionTime { get; set; }

    }
    public enum AppointmentStatus
    {
        Pending,
        Cancelled,
        Completed
    }
}
