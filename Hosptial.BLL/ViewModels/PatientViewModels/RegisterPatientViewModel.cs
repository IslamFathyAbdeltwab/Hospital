using Hosptial.BLL.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.PatientViewModels
{
    public class RegisterPatientViewModel:UserViewModel
    {
        [Required(ErrorMessage = "Street is required")]
        [StringLength(200, MinimumLength = 3,
        ErrorMessage = "Street must be between 3 and 200 characters")]
        public string Street { get; set; }


        [Required(ErrorMessage = "City is required")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "City must be between 2 and 100 characters")]
        public string City { get; set; }


        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Country must be between 2 and 100 characters")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

    }
}
