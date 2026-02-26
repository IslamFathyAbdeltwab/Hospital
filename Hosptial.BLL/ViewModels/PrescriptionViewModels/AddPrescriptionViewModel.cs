using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.PrescriptionViewModels
{
    public class AddPrescriptionViewModel
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>(); 
    }
}
