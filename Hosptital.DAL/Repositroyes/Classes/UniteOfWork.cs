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
    public class UniteOfWork : IUniteOfWork
    {
        private readonly HospitalDbContext hospitalDbContext;

        public UniteOfWork(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;
        }



        public int SaveChanges()
        {
            return hospitalDbContext.SaveChanges();
        }
        Dictionary<Type, object> Repo = new Dictionary<Type, object>();
        public IGenaricRepo<TEntity> GetGenaricRepo<TEntity>() where TEntity : BaseEntity
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
    }
}
