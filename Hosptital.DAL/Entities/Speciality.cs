using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities
{
    public class Speciality : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Doctor> Doctors { get; set; } = new HashSet<Doctor>();
      
    }
}
