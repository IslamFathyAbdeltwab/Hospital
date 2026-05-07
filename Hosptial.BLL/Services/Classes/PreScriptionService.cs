using AutoMapper;
using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.Specification;
using Hosptial.BLL.ViewModels.PrescriptionViewModels;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Migrations;
using Hosptital.DAL.Repositroyes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Services.Classes
{
    public class PreScriptionService : IPrescriptionService
    {
        private readonly IUniteOfWork uniteOfWork;
        private readonly IMapper mapper;
        private readonly IAIService aiService;

        public PreScriptionService(IUniteOfWork uniteOfWork,IMapper mapper, IAIService aiService)
        {
            this.uniteOfWork = uniteOfWork;
            this.mapper = mapper;
            this.aiService = aiService;
        }
        public async Task<bool> Add(AddPrescriptionViewModel prescription)
        {
            if (prescription is null) return false;
            var pre = new Prescription
            {
                DoctorId = prescription.DoctorId,
                PatientId = prescription.PatientId,
                Treatments = prescription.Treatments,
                Diagnosis = prescription.Diagnosis,
            };
            pre.AiExplanation = await aiService.ExplainPrescription(prescription);


            uniteOfWork.GetGenaricRepo<Prescription>().Add(pre);
           
            return await uniteOfWork.SaveChangesAsync() > 0;
        }

        public async Task<PrescriptionViewModel> Get(int id)
        {
            if (id <= 0) return null;
            var spec = new PrescriptionSpecification(id);
            var prescription =await uniteOfWork.GetGenaricRepo<Prescription>().Get(spec);
            return mapper.Map<PrescriptionViewModel>(prescription);
        }

        public async Task<List<PrescriptionViewModel>> GetAll(int patientId)
        {
            var spec = new PrescriptionGetAllSpecification(patientId);

            var prescriptions=await uniteOfWork.GetGenaricRepo<Prescription>().GetAll(spec);


             
           
            return mapper.Map<List<PrescriptionViewModel>>(prescriptions);

        }
        public async Task<List<Prescription>> GetDoctorPationtsWithPrescriptions(int doctorId)
        {
            var spec = new DoctorPrescriptionSpecification(doctorId);

            var prescriptions = await uniteOfWork.GetGenaricRepo<Prescription>().GetAll(spec);

            return prescriptions;
        }

    }
}
