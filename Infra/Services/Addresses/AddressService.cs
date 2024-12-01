using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.DTOs.Addresses;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services.Addresses;
using Core.Specification.Addresses;

namespace Infra.Services.Addresss
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<Address>> UpdateAddressAsync(AddressUpdateDTO dto)
        {
            var response = new GenericResponse<Address>();
            // check to see if address exists
            var spec = new AddressByIdSpecification(dto.Id);
            var address = await _unitOfWork.Repository<Address>().GetEntityWithSpec(spec);

            if (address != null)
            {
                response.Error = new MessageResponse { Message = $"Endereço com {dto.Id} não encontrado!" };
                return response;
            }

            address = _mapper.Map<AddressUpdateDTO, Address>(dto, address);
            address.UpdateAt = DateTime.UtcNow;
            _unitOfWork.Repository<Address>().Update(address);

            // save to db
            var result = await _unitOfWork.Complete();

            if (result.Error != null)
            {
                response.Error = result.Error;
            };

            // return order
            return response;
        }

        public async Task<GenericResponse<Address>> CreateAddressAsync(Address address)
        {
            var response = new GenericResponse<Address>();

            _unitOfWork.Repository<Address>().Add(address);

            // save to db
            var result = await _unitOfWork.Complete();

            if (result.Error != null)
            {
                response.Error = result.Error;
            };

            // return order
            return response;
        }

        public async Task<GenericResponse<bool>> DeleteAddressAsync(int id)
        {
            var response = new GenericResponse<bool>();

            // check to see if address exists
            var spec = new AddressByIdSpecification(id);
            var address = await _unitOfWork.Repository<Address>().GetEntityWithSpec(spec);

            _unitOfWork.Repository<Address>().Delete(address);
            // save to db
            var result = await _unitOfWork.Complete();
            response.Data = result.Data;

            if (result.Error != null)
            {
                response.Error = result.Error;
            };

            // return address
            return response;

        }
    }
}