using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Addresses;
using Core.Entities;

namespace Core.Interfaces.Services.Addresses
{
    public interface IAddressService
    {
        Task<GenericResponse<Address>> CreateAddressAsync(Address dto);
        Task<GenericResponse<Address>> UpdateAddressAsync(AddressUpdateDTO dto);
        Task<GenericResponse<bool>> DeleteAddressAsync(int id);
    }
}