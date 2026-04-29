using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Entities
{
    public class Admin :BaseEntity
    {
        public string Name { get; set; } = string.Empty;   


        public ApplicationUser User { get; set; }
            public int UserId { get; set; }
    }
}
