using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.DTOs.Clients;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services.Clients;
using Core.Specification.Clients;

namespace Infra.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<GenericResponse<Client>> CreateClientAsync(Client client)
        {
            var response = new GenericResponse<Client>();

            _unitOfWork.Repository<Client>().Add(client);
            // save to db
            var result = await _unitOfWork.Complete();

            if (result.Error != null)
            {
                response.Error = result.Error;
            };

            // return order
            return response;
        }
        public async Task<GenericResponse<Client>> UpdateClientAsync(ClientUpdateDTO dto)
        {
            var response = new GenericResponse<Client>();
            // check to see if client exists
            var spec = new ClientByIdSpecification(dto.Id);
            var client = await _unitOfWork.Repository<Client>().GetEntityWithSpec(spec);

            if (client != null)
            {
                response.Error = new MessageResponse { Message = $"Cliente com {dto.Id} não encontrado!" };
                return response;
            }

            client = _mapper.Map<ClientUpdateDTO, Client>(dto, client);
            client.UpdateAt = DateTime.UtcNow;
            _unitOfWork.Repository<Client>().Update(client);

            // save to db
            var result = await _unitOfWork.Complete();

            if (result.Error != null)
            {
                response.Error = result.Error;
            };

            // return order
            return response;
        }

        public async Task<GenericResponse<bool>> DeleteClientAsync(int id)
        {
            var response = new GenericResponse<bool>();

            // check to see if client exists
            var spec = new ClientByIdSpecification(id);
            var client = await _unitOfWork.Repository<Client>().GetEntityWithSpec(spec);

            _unitOfWork.Repository<Client>().Delete(client);
            // save to db
            var result = await _unitOfWork.Complete();
            response.Data = result.Data;

            if (result.Error != null)
            {
                response.Error = result.Error;
            };

            // return client
            return response;

        }
    }
}