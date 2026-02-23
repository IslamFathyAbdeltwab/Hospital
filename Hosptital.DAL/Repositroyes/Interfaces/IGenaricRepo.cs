using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Repositroyes.Interfaces
{
    public interface IGenaricRepo<TEntity>where TEntity : BaseEntity
    {
        Task<TEntity> Get(int id);
        Task<List<TEntity?>> GetAll();

        void Add(TEntity e);
        void Update(TEntity e);
        void Delete(TEntity e);
    }
}
