using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.PatientViewModels
{
    public class AddPatientViewModel
    {
        // ===== User Info =====
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }

        // ===== Patient Info =====
        public DateOnly DateOfBirth { get; set; }

        // ===== Address Info =====
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
