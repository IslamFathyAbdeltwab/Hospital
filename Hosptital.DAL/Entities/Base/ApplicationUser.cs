using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities.Base
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Gender Gender { get; set; }

        public ICollection<Appointment> PatientAppointments { get; set; }
        public ICollection<Appointment> DoctorAppointments { get; set; }

        public ICollection<MedicalFile> MedicalFiles { get; set; }

    }
    public enum Gender
    {
        male = 1,
        Female = 2,
    }
}
