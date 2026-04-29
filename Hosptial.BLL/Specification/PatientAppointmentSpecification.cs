using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Specification
{
    public class PatientAppointmentSpecification:BaseSpecification<Booking>
    {
        public PatientAppointmentSpecification(int patientId) : base(b => b.PatientId == patientId)
        {
            AddInclude(b => b.DoctorAvailability);
            AddInclude(b => b.Patient);
            //AddInclude(b => b.Doct);
        }
    }
}
