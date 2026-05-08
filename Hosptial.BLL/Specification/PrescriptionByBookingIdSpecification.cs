using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Specification
{
    public class PrescriptionByBookingIdSpecification : BaseSpecification<Prescription>
    {
        public PrescriptionByBookingIdSpecification(int bookingId) : base(p => p.BookingId == bookingId)
        {
            AddInclude(p => p.Treatments);
            AddInclude(p => p.Doctor);
            AddInclude(p => p.Doctor.User);
        }
    }
}
