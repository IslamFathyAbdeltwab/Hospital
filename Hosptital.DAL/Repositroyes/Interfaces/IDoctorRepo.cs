using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Repositroyes.Interfaces
{
    public interface IDoctorRepo:IGenaricRepo<Doctor>
    {
        public Task<List<Doctor>> GetIncludeSpeciality(int specialityId);
        public Task<Doctor?> GetIncludeSpecialityAndAppointments(int id);
    }
}
