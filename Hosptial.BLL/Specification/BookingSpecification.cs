using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Specification
{
    internal class BookingSpecification : BaseSpecification<Booking>
    {
        public BookingSpecification(int avlId) : base(b=>b.DoctorAvailabilityId==avlId)
        {
            AddInclude(b => b.Patient);
            AddInclude(b => b.Patient.User);
        }

        public BookingSpecification(Expression<Func<Booking, bool>> criteria) : base(criteria)
        {
            AddInclude(b => b.Patient);
            AddInclude(b => b.Patient.User);
            AddInclude(b => b.DoctorAvailability);
        }
    }
}
