using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities
{
    public class Consultation : BaseEntity
    {

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
