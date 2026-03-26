using Hosptital.DAL.Entities;
using Hosptital.DAL.Entities.Base;
using Hosptital.DAL.Repositroyes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Specification
{
    public class PrescriptionSpecification:BaseSpecification<Prescription>
    {
        public PrescriptionSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Treatments);
            AddInclude(p => p.Doctor);
           
        }
    }
}
