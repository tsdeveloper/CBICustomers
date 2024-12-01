using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> crieria)
        {
            Crieria = crieria;
            
        }

        public Expression<Func<T, bool>> Crieria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = 
            new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }
        
        public Expression<Func<T, object>> OrderbyByDescending { get;  private  set; }
        public int Take { get;  private set;}
        public int Skip { get;  private set;}
        public bool IsPagingEnabled { get;  set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        
        protected void AddOrderby(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        
        protected void AddOrderbyByDescending(Expression<Func<T, object>> orderbyByDescendingExpression)
        {
            OrderbyByDescending = orderbyByDescendingExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}