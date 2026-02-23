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

        public int ConsultationId { get; set; }
        public Consultation Consultation { get; set; }
        public string MedicationName { get; set; }
        public string Notes { get; set; }
       
    }
}
