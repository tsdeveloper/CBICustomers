using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Clients;
using Core.Entities;

namespace Core.Interfaces.Services.Clients
{
    public interface IClientService
    {
        Task<GenericResponse<Client>> CreateClientAsync(Client entity);
        Task<GenericResponse<Client>> UpdateClientAsync(ClientUpdateDTO dto);
        Task<GenericResponse<bool>> DeleteClientAsync(int id);
    }
}