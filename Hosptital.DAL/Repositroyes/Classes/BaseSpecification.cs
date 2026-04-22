using Hosptital.DAL.Entities.Base;
using Hosptital.DAL.Repositroyes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Repositroyes.Classes
{
    public abstract class BaseSpecification<TEntity> : Interfaces.BaseSpecification<TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; }

        public List<Expression<Func<TEntity, object>>> Includes { get; private set; } = [];

        public BaseSpecification(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
        }
        protected BaseSpecification()
        {
            
        }
        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
