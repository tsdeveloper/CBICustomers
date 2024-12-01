using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specification.Clients.SpecParams;

namespace Core.Specification.Clients
{
    public class ClientWithAddressSpecification: BaseSpecification<Client>
    {
        public ClientWithAddressSpecification(ClientSpecParams specParams)
            : base(x =>
            string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)
            )
        {
       
            AddOrderby(x => x.Name);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if (!string.IsNullOrWhiteSpace(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "desc" :
                        AddOrderbyByDescending(p => p.Name);
                        break;
                    default:
                        AddOrderby(p => p.Name);
                        break;
                        
                }

            };
        }
    }
}