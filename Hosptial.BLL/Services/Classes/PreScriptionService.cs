using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.PrescriptionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Classes
{
    public class PreScriptionService : IPrescriptionService
    {
        public Task<bool> Add(AddPrescriptionViewModel prescription)
        {
            throw new NotImplementedException();
        }

        public Task<PrescriptionViewModel> Get(int doctorId, int patientId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PrescriptionViewModel>> GetAll(int patientId)
        {
            throw new NotImplementedException();
        }
   
    }
}
