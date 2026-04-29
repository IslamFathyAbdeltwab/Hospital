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
    public class DoctorGetAllSpecification:BaseSpecification<Doctor>
    {
        public DoctorGetAllSpecification(int specialityId):base(d=>d.SpecialityId==specialityId)
        {
            AddInclude(d => d.User);
            AddInclude(d => d.Speciality);
            
        }
        public DoctorGetAllSpecification(Expression<Func<Doctor, bool>> criteria) : base(criteria)
        {
            AddInclude(d => d.User);
            AddInclude(d => d.Speciality);
        }

        public DoctorGetAllSpecification() : base()
        {
            AddInclude(d => d.User);
            AddInclude(d => d.Speciality);

        }
    }
}
