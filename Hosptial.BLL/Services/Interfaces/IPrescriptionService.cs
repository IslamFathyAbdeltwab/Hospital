using Hosptial.BLL.ViewModels.PrescriptionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Interfaces
{
    public interface IPrescriptionService
    {
        public Task<bool> Add(AddPrescriptionViewModel prescription);

        public Task<PrescriptionViewModel> Get(int doctorId, int patientId);

        public Task<List<PrescriptionViewModel>> GetAll(int patientId);
    }
}
