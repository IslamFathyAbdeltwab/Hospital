using Hosptital.DAL.Data.Contexts;
using Hosptital.DAL.Entities.Base;
using Hosptital.DAL.Repositroyes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Repositroyes.Classes
{
    public class GenaricRepo<TEntity>(HospitalDbContext hospitalDbContext): IGenaricRepo<TEntity> where TEntity : BaseEntity
    {
        public void  Add(TEntity e)
        {
              hospitalDbContext.Set<TEntity>().Add(e);
        }

        public void Delete(TEntity e)
        {
             hospitalDbContext.Set<TEntity>().Remove(e);
        }

        public async Task<TEntity?> Get(int id)
        {
          return await hospitalDbContext.Set<TEntity>().FindAsync(id);
        }

      

        public async Task<List<TEntity?>> GetAll(Func<TEntity, bool>? Condition = null)
        {
            if (Condition is null)
            {
                return await hospitalDbContext.Set<TEntity>().ToListAsync();
            }
            return  hospitalDbContext.Set<TEntity>().Where(Condition).ToList();
        }

        public void Update(TEntity e)
        {
           hospitalDbContext.Set<TEntity>().Update(e);
        }
    }
}
