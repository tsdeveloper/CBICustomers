using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using AutoMapper;
using Core.DTOs.Addresses;
using Core.DTOs.Clients;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services.Addresses;
using Core.Specification.Addresses;
using Core.Specification.Clients.SpecParams;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // [Authorize]
    public class AddressController : BaseAPIController
    {
        private readonly IMapper _mapper;
        private readonly IValidator<AddressCreateDTO> _validatorAddressCreateDTO;
        private readonly IValidator<AddressUpdateDTO> _validatorAddressUpdateDTO;
        private readonly IAddressService _serviceAddress;
        private readonly IGenericRepository<Address> _genericAddress;

        public AddressController(IMapper mapper,
        IValidator<AddressCreateDTO> validatorAddressCreateDTO,
        IValidator<AddressUpdateDTO> validatorAddressUpdateDTO,
        IAddressService serviceAddress,
        IGenericRepository<Address> genericAddress)
        {
            _mapper = mapper;
            _validatorAddressCreateDTO = validatorAddressCreateDTO;
            _validatorAddressUpdateDTO = validatorAddressUpdateDTO;
            _serviceAddress = serviceAddress;
            _genericAddress = genericAddress;
        }


        [HttpGet("all")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginationWithReadOnyList<AddressReturnDTO>>> GetAddresss(
            [FromQuery] AddressSpecParams paramsQuery)
        {
            var spec = new AddressWithClientSpecification(paramsQuery);
            var countSpec = new AddressWithFiltersForCountSpecification(paramsQuery);
            var totalItems = await _genericAddress.CountAsync(countSpec);

            var addressList = await _genericAddress.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<AddressReturnDTO>>(addressList);

            return Ok(new PaginationWithReadOnyList<AddressReturnDTO>(paramsQuery.PageIndex,
                paramsQuery.PageSize, totalItems, data));
        }

        [HttpGet("details/{clientId:guid}/{id:int}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressReturnDTO>> GetAddressById(Guid clientId, int id)
        {
            var spec = new AddressWithClientSpecification(new AddressSpecParams { Id = id, ClientId = clientId.ToString() });
            var result = await _genericAddress.GetEntityWithSpec(spec);

            return Ok(_mapper.Map<AddressReturnDTO>(result));
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressReturnDTO>> PostAddressCreate(AddressCreateDTO dto)
        {
            var validator = _validatorAddressCreateDTO.Validate(dto);

            if (!validator.IsValid)
                return BadRequest(new ApiResponse(400, validator.Errors.FirstOrDefault().ErrorMessage));

            var address = _mapper.Map<Address>(dto);
            var result = await _serviceAddress.CreateAddressAsync(address);

            if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));
            var resultDto = _mapper.Map<AddressReturnDTO>(address);

            return CreatedAtAction(nameof(GetAddressById), new {clientId = address.ClientId  ,id = resultDto.Id,  }, resultDto);
        }

        [HttpPut("editar")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressReturnDTO>> PutddressUpdate(AddressUpdateDTO dto)
        {
            var validator = _validatorAddressUpdateDTO.Validate(dto);

            if (!validator.IsValid)
                return BadRequest(new ApiResponse(400, validator.Errors.FirstOrDefault().ErrorMessage));

            var result = await _serviceAddress.UpdateAddressAsync(dto);

            if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));
            return NoContent();

        }

        [HttpDelete("remover/{id:int}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressReturnDTO>> DeleteAddressById(int id)
        {
            var result = await _serviceAddress.DeleteAddressAsync(id);

            if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));
            return NoContent();

        }
    }
}