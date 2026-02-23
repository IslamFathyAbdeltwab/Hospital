using Hosptital.DAL.Repositroyes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Data.Contexts
{
    public class DbInitlizer : IDbInitlizer
    {
        private readonly HospitalDbContext hospitalDb;

        public DbInitlizer(HospitalDbContext hospitalDb)
        {
            this.hospitalDb = hospitalDb;
        }
        public async Task Initialize()
        {
            var pendingMigrations = await hospitalDb.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                hospitalDb.Database.Migrate();
            }

        }

        public Task SeedData()
        {
            throw new NotImplementedException();
        }
    }
}
