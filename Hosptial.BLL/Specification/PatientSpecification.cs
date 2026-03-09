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
    public class PatientSpecification : BaseSpecification<Patient>
    {
        public PatientSpecification(int id) : base(p=>p.Id==id)
        {
            AddInclude(p => p.User);
        }
    }
}
