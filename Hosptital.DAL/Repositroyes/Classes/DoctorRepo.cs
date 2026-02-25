using Hosptital.DAL.Data.Contexts;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Repositroyes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Repositroyes.Classes
{
    public class DoctorRepo(HospitalDbContext context) : GenaricRepo<Doctor>(context), IDoctorRepo
    {
        private readonly HospitalDbContext context = context;

        public async Task<List<Doctor>> GetIncludeSpeciality(int specialityId)
        {
            return await context.Doctors.Where(d=>d.SpecialityId==specialityId).Include(d => d.Speciality).ToListAsync() ;
                        
        }

        public async Task<Doctor?> GetIncludeSpecialityAndAppointments(int id)
        {
            return await context.Doctors
                .Include(d => d.Speciality)
                .Include(d => d.DoctorAvailabilities)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
