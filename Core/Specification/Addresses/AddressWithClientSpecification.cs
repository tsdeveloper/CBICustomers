using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specification.Clients.SpecParams;

namespace Core.Specification.Addresses
{
    public class AddressWithClientSpecification: BaseSpecification<Address>
    {
        public AddressWithClientSpecification(AddressSpecParams specParams)
            : base(x =>
            (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
            (string.IsNullOrEmpty(specParams.City) || x.City == specParams.City) &&
            (string.IsNullOrEmpty(specParams.ZipCode) || x.ZipCode == specParams.ZipCode) &&
            (string.IsNullOrEmpty(specParams.State) || x.State == specParams.State)
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