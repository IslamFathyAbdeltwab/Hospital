using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Specification
{
    public class AdminWithUserSpecification : BaseSpecification<Admin>
    {
        public AdminWithUserSpecification(int id) : base(a => a.Id == id)
        {
            AddInclude(a => a.User);
        }

        public AdminWithUserSpecification() : base(null)
        {
            AddInclude(a => a.User);
        }
    }
}
