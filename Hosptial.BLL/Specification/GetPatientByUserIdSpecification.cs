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
    public class GetPatientByUserIdSpecification : BaseSpecification<Patient>
    {

        public GetPatientByUserIdSpecification(int userID):base(p => p.UserId == userID)
        {
            AddInclude(p => p.User);

        }

    }
}
