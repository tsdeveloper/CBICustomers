using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specification.Clients.SpecParams;

namespace Core.Specification.Addresses
{
    public class AddressWithFiltersForCountSpecification: BaseSpecification<Address>
    {
        public AddressWithFiltersForCountSpecification(AddressSpecParams specParams)
            : base(x =>
             (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
            (string.IsNullOrEmpty(specParams.City) || x.City == specParams.City) &&
            (string.IsNullOrEmpty(specParams.ZipCode) || x.ZipCode == specParams.ZipCode) &&
            (string.IsNullOrEmpty(specParams.State) || x.State == specParams.State)
            )
        {
        }
    }
}