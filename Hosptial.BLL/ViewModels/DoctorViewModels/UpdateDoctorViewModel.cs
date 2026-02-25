using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.DoctorViewModels
{
    public class UpdateDoctorViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Years of experience is required")]
        [Range(0, 60, ErrorMessage = "Years of experience must be between 0 and 60")]
        public int YearsOfExperienc { get; set; }

        [Required(ErrorMessage = "Bio is required")]
        [StringLength(500, ErrorMessage = "Bio cannot exceed 500 characters")]
        public string Bio { get; set; }

        [Required(ErrorMessage = "Speciality is required")]
        public int SpecialityId { get; set; }
    }
}
