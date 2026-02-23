using Hosptial.BLL.ViewModels.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface IPatientService
    {
        public Task Register(RegisterPatientViewModel patient);

        /*
         * 
         * 🔑 Auth

               Register
               
               Login
               
               Change Password
               
               👤 Profile
               
               Get Profile
               
               Update Profile
               
               📂 Files
               
               Upload File
               
               List Files
               
               Delete File
               
               📅 Appointments
               
               View Appointments
               
               Cancel Appointment
               
               Reschedule Appointment
         
         
         */
    }
}
