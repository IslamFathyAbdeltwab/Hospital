using Hosptital.DAL.Data.Contexts;
using Hosptital.DAL.Entities;
using Hosptital.DAL.Migrations;
using Hosptital.DAL.Repositroyes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Repositroyes.Classes
{
    public class DoctorRepo(HospitalDbContext context ) : GenaricRepo<Doctor>(context), IDoctorRepo
    {
        private readonly HospitalDbContext context = context;

       
    }
}
