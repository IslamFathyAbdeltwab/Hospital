using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.BookingViewModels
{
    public class AddBookViewModel
    {
        [Required]
        public AppointmentStatus Status { get; set; }
        [Required]

        public int DoctorAvailabilityId { get; set; }
        [Required]

        public int PatientId { get; set; }

    }
}
