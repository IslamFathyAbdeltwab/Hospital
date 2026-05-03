using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Specification
{
    public class DoctorPrescriptionSpecification : BaseSpecification<Prescription>
    {
        public DoctorPrescriptionSpecification(int doctorId)
            : base(p => p.DoctorId == doctorId)
        {
            AddInclude(p => p.Patient);
            AddInclude(p => p.Patient.User);
            AddInclude(p => p.Patient.Address);
            AddInclude(p => p.Treatments);
        }
    }
}
