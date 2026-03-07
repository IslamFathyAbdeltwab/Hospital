using Hosptital.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Repositroyes.Interfaces
{
    public interface IBaseSpecification <TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; }

        public List<Expression<Func<TEntity, object>>> Includes { get; }
    }
}
