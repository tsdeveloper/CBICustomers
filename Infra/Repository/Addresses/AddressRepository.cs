using Core.Entities;
using Core.Interfaces.Repositories;
using Infra.Data;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository.Addresses;
public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(CBICustomersContext context) : base(context) { }
}
