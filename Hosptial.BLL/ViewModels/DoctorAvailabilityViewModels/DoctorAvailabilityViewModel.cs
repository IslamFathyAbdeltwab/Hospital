using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels
{
    public class DoctorAvailabilityViewModel
    {
        public DoctorAvailability doctorAvailability;
        public bool Book_Complete { get; set; }
    }
}
