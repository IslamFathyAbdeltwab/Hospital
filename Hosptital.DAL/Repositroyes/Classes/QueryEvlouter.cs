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
    public static class QueryEvlouter
    {
        public static IQueryable<TEntity> ApplySpecification<TEntity>(IQueryable<TEntity> query, Interfaces.BaseSpecification<TEntity> specification) where TEntity : BaseEntity
        {
            var Entrypoint = query;
            if (specification.Criteria != null)
            {
                Entrypoint = Entrypoint.Where(specification.Criteria);
            }
            if (specification.Includes is not null)
            {
                if (specification.Includes.Any())
                {
                    Entrypoint = specification.Includes.Aggregate(Entrypoint, (current, include) => current.Include(include));
                }

            }
            if (specification.OrderBy is not null)
            {
                Entrypoint = specification.OrderBy.Aggregate(Entrypoint, (current, orderBy) => current.OrderBy(orderBy));
            }
            if (specification.OrderByDescending is not null)
            {
                Entrypoint = specification.OrderByDescending.Aggregate(Entrypoint, (current, orderByDescending) => current.OrderByDescending(orderByDescending));
            }

            return Entrypoint;
        }
    }
}
