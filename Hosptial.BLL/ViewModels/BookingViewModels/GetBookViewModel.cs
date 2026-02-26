using Hosptial.BLL.ViewModels.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.BookingViewModels
{
    public class GetBookViewModel
    {
        public int Id { get; set; }
        public DateTime ConsultationTime { get; set; }
        public DateTime End { get; set; }
        public PatientViewModel PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
    }
}
