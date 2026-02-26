using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.PrescriptionViewModels
{
    public class PrescriptionViewModel
    {
        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
