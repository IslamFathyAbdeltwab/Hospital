using Hosptial.BLL.ViewModels.PrescriptionViewModels;
using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface IAIPatientSummaryService
    {
        Task<string> GenerateSummary(List<PrescriptionViewModel> prescriptions);
    }
}
