using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Specification
{
    public class PrescriptionGetAllSpecification:BaseSpecification<Prescription>
    {
        public PrescriptionGetAllSpecification(int patientId) : base(p => p.PatientId == patientId)
        {
            AddInclude(p => p.Doctor);
            AddInclude(p => p.Treatments);
        }
    }
}
