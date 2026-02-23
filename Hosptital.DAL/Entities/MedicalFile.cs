using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities
{
    public class MedicalFile : BaseEntity
    {


        public string FileName { get; set; }
        public string FilePath { get; set; }

        public string PatientId { get; set; }
        public ApplicationUser Patient { get; set; }


    }
}
