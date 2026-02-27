using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels
{
    public class UpdateDoctorAvailabilityViewModel
    {
        [Required]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        [Required]
        public int DoctorAvailabilityId { get; set; }

        [Required(ErrorMessage = "Available date and time is required")]
        [DataType(DataType.DateTime)]
        public DateTime AvailableFrom { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Max patients must be between 1 and 100")]
        public int MaxPatients { get; set; }

        [Required]
        [Range(5, 240, ErrorMessage = "Session duration must be between 5 and 240 minutes")]
        public int SessionDurationMinutes { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 100000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
    }
}
