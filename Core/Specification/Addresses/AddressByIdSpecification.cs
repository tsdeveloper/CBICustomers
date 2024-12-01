using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specification.Clients.SpecParams;

namespace Core.Specification.Addresses
{
    public class AddressByIdSpecification : BaseSpecification<Address>
    {
        public AddressByIdSpecification(int id)
            : base(x => x.Id.Equals(id)
            )
        {

        }
    }
}