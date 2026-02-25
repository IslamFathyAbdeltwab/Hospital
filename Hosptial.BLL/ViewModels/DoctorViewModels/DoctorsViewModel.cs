using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels.DoctorViewModels
{
    public class DoctorsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; } 
        public Gender Gender { get; set; }
        public int YearsOfExperience { get; set; }
        public string Bio { get; set; }
        public string Phone { get; set; }
    }
}
