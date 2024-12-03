using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.DTOs.Addresses;
using Core.DTOs.Clients;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Interfaces.Services.Clients;
using Core.Specification.Addresses;
using Core.Specification.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unitOfWork,
        IMapper mapper,
        UserManager<Client> userManager,
        SignInManager<Client> signInManager,
        ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        public async Task<GenericResponse<ClientReturnRegisterDto>> CreateClientAsync(ClientRegisterDto dto)
        {
            var response = new GenericResponse<ClientReturnRegisterDto>();

            var client = _mapper.Map<Client>(dto);

            var result = await _userManager.CreateAsync(client, dto.Password);
            // save to db

            if (!result.Succeeded)
            {
                response.Error = new MessageResponse { Message = result.Errors.FirstOrDefault().Description };
                return response;
            };

            var clientLogin = _mapper.Map<ClientReturnRegisterDto>(client);
            clientLogin.Token = _tokenService.CreateToken(client);

            response.Data = clientLogin;

            // return order
            return response;
        }
        public async Task<GenericResponse<Client>> UpdateClientAsync(ClientUpdateDTO dto)
        {
            var response = new GenericResponse<Client>();
            var specAddress = new AddressByIdSpecification(dto.Address != null ? dto.Address.Id : 0);
            var address = await _unitOfWork.Repository<Address>().GetEntityWithSpec(specAddress);
            // check to see if client exists
            var spec = new ClientByIdSpecification(dto.Id);
            var client = await _unitOfWork.Repository<Client>().GetEntityWithSpec(spec);

            if (client == null)
            {
                response.Error = new MessageResponse { Message = $"Cliente com {dto.Id} não encontrado!" };
                return response;
            }

            client = _mapper.Map<ClientUpdateDTO, Client>(dto, client);

            client.UpdateAt = DateTime.UtcNow;
            _unitOfWork.Repository<Client>().Update(client);

           if (client.Address != null)
           {
             if (client.Address.Id == 0)
                 _unitOfWork.Repository<Address>().Add(client.Address);
             else
             {
                 address = _mapper.Map<Address, Address>(address, client.Address);
 
                 if (address == null)
                 {
                     response.Error = new MessageResponse { Message = $"Endereço com ID {client.Address.Id} não encontrado!" };
                     return response;
                 }
 
                 address.UpdateAt = DateTime.UtcNow;
                 _unitOfWork.Repository<Address>().Update(address);
 
             }
 
           }
            // save to db
            var result = await _unitOfWork.Complete();

            if (result.Error != null)
            {
                response.Error = result.Error;
            }

            response.Data = client;

            // return order
            return response;
        }

        public async Task<GenericResponse<bool>> DeleteClientAsync(string id)
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