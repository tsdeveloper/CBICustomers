using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specification.Clients.SpecParams;

namespace Core.Specification.Clients
{
    public class ClientWithFiltersForCountSpecification : BaseSpecification<Client>
    {
        public ClientWithFiltersForCountSpecification(ClientSpecParams specParams)
            : base(x =>
          string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)
            )
        {
        }
    }
}