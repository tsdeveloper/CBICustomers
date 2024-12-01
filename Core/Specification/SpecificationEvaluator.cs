using System.Linq;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Specification
{
    public class SpecificationEvaluator<TEntity> where TEntity : class, IBaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Crieria != null)
                query = query.Where(spec.Crieria);

            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            
            if (spec.OrderbyByDescending != null)
                query = query.OrderByDescending(spec.OrderbyByDescending);

            if (spec.IsPagingEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}