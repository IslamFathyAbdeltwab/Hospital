using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities
{
    public class Doctor : BaseEntity
    {
        public Speciality Speciality { get; set; }
        public int SpecialityId { get; set; }
        public int YearsOfExperienc { get; set; }
        public string Bio { get; set; }
        public ApplicationUser User { get; set; }

        public int UserId { get; set; }

    }
}
