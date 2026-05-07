using Hosptial.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface IAISymptomCheckerService
    {
        Task<SymptomResponseDto> CheckSymptoms(string symptoms);
    }
}
