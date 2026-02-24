using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities
{
    public class DoctorAvailability : BaseEntity
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime AvailableFrom { get; set; }
        public int MaxPatients { get; set; }
        public int SessionDurationMinutes { get; set; }

        


    }
}
