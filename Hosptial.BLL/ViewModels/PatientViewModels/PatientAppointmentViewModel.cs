using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.PatientViewModels
{
    public class PatientAppointmentViewModel
    {
        public int DoctorId { get; set; }
        public int BookingId { get; set; }
        public string DoctorName { get; set; }
        public string ConsultationTime { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }

    }
}
