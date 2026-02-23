using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Repositroyes.Interfaces
{
    public interface IUniteOfWork<TEntity> where TEntity : BaseEntity
    {
        IGenaricRepo<TEntity> GetGenaricRepo();
        int SaveChanges();
    }
}
