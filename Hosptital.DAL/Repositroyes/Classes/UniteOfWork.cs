using Hosptital.DAL.Data.Contexts;
using Hosptital.DAL.Entities.Base;
using Hosptital.DAL.Repositroyes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Repositroyes.Classes
{
    public class UniteOfWork<TEntity> : IUniteOfWork<TEntity> where TEntity : BaseEntity
    {
        private readonly HospitalDbContext hospitalDbContext;

        public UniteOfWork(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;
        }

        Dictionary<Type, object> Repo = new Dictionary<Type, object>();
        public IGenaricRepo<TEntity> GetGenaricRepo()
        {
            var genaricRepo = new GenaricRepo<TEntity>(hospitalDbContext);
            if (Repo.ContainsKey(typeof(TEntity)))
            {
                return (IGenaricRepo<TEntity>)Repo[typeof(TEntity)];
            }
            else
            {
                Repo.Add(typeof(TEntity), genaricRepo);
                return genaricRepo;
            }
        }

        public int SaveChanges()
        {
            return hospitalDbContext.SaveChanges();
        }
    }
}
