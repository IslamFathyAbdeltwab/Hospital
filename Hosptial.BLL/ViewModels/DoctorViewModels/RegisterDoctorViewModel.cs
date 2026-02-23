using Hosptial.BLL.ViewModels.Common;
using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.DoctorViewModels
{
    public class RegisterDoctorViewModel: UserViewModel
    {


        [Required(ErrorMessage = "Speciality is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid speciality")]
        public int SpecialityId { get; set; }

        [Required(ErrorMessage = "Years of experience is required")]
        [Range(0, 60, ErrorMessage = "Years of experience must be between 0 and 60")]
        public int YearsOfExperience { get; set; }

        [Required(ErrorMessage = "Bio is required")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "Bio must be between 10 and 500 characters")]
        public string Bio { get; set; }

    }
}
