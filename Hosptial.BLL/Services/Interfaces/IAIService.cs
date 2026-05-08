using Hosptial.BLL.ViewModels.PrescriptionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    
        public interface IAIService
        {
            Task<string> ExplainPrescription(AddPrescriptionViewModel prescription);
        }
    
}
