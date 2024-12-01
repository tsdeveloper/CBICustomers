using Core.Entities;
using Core.Interfaces.Repositories;
using Infra.Data;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository.Clients;
public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(CBICustomersContext context) : base(context) { }
}
