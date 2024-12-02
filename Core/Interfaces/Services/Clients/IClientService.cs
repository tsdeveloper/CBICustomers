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
        Task<GenericResponse<ClientReturnRegisterDto>> CreateClientAsync(ClientRegisterDto dto);
        Task<GenericResponse<Client>> UpdateClientAsync(ClientUpdateDTO dto);
        Task<GenericResponse<bool>> DeleteClientAsync(string id);
    }
}