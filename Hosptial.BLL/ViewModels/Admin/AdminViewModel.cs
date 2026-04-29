using Hosptial.BLL.ViewModels.Common;
using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.Admin
{
    public class AdminViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Gender Gender { get; set; }

        public string Phone { get; set; } = string.Empty;



    }
}
