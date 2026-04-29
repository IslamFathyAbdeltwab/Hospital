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
    public class DoctorSpecification : BaseSpecification<Doctor>
    {
        public DoctorSpecification(int id) : base(d => d.Id == id)
        {
            AddInclude(d => d.User);
            AddInclude(d => d.Speciality);
        }
        public DoctorSpecification( Expression<Func<Doctor, bool>>criteria) :base(criteria)
        {
            AddInclude(d => d.User);
            AddInclude(d => d.Speciality);

        }
    }
}
