using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.ViewModels
{
    public class SymptomResponseDto
    {
        public string Speciality { get; set; }

        public List<string> PossibleDiseases { get; set; }

        public string Advice { get; set; }
    }
}
