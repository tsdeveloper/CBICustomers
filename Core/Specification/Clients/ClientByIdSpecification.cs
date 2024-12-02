using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specification.Clients.SpecParams;

namespace Core.Specification.Clients
{
    public class ClientByIdSpecification: BaseSpecification<Client>
    {
        public ClientByIdSpecification(string id)
            : base(x => x.Id.Equals(id)
            )
        {
       
        }
    }
}