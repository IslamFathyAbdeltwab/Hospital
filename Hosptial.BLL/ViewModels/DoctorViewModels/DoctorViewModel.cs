using Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.DoctorViewModels
{
    public class DoctorViewModel:DoctorsViewModel
    {
        
        public List<DoctorAvailabilityViewModel> DoctorAvailabilites { get; set; } 
       
    }
}
