using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Repositroyes.Interfaces
{
    public interface IUniteOfWork
    {
        IGenaricRepo<TEntity> GetGenaricRepo<TEntity>() where TEntity : BaseEntity ;
        int SaveChanges();
    }
}
