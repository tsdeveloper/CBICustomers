using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.DTOs.Addresses;
using Core.DTOs.Clients;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Interfaces.Services.Clients;
using Core.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseAPIController
{
  private readonly UserManager<Client> _userManager;
  private readonly SignInManager<Client> _signInManager;
  private readonly ITokenService _tokenService;
  private readonly IMapper _mapper;
  private readonly IClientService _serviceClient;
  private readonly IValidator<ClientRegisterDto> _validatorClientRegisterDto;
  private readonly IValidator<ClientUpdateDTO> _validatorClientUpdateDTO;
  private readonly IValidator<AddressCreateDTO> _validatorAddressCreateDTO;
  private readonly IValidator<AddressUpdateDTO> _validatorAddressUpdateDTO;
  private readonly ITokenService _serviceToken;
    public AccountController(UserManager<Client> userManager,
     SignInManager<Client> signInManager,
        ITokenService tokenService,
        IMapper mapper,
        IClientService serviceClient,
        IValidator<ClientUpdateDTO> validatorClientUpdateDTO,
        IValidator<AddressCreateDTO> validatorAddressCreateDTO,
        IValidator<AddressUpdateDTO> validatorAddressUpdateDTO,
        IValidator<ClientRegisterDto> validatorClientRegisterDto,
        ITokenService serviceToken)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _userManager = userManager;
        _serviceClient = serviceClient;
        _validatorClientUpdateDTO = validatorClientUpdateDTO;
        _validatorAddressCreateDTO = validatorAddressCreateDTO;
        _validatorAddressUpdateDTO = validatorAddressUpdateDTO;
        _validatorClientRegisterDto = validatorClientRegisterDto;
        _serviceToken = serviceToken;
    }

    [Authorize]
  [HttpGet]
  public async Task<ActionResult<ClientReturnDTO>> GetCurrentUser()
  {
    var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

    return _mapper.Map<ClientReturnDTO>(user);
  }

  [HttpPost("login")]
  public async Task<ActionResult<ClientReturnRegisterDto>> Login(ClientLoginDto loginDto)
  {
    var user = await _userManager.GetUserByEmailWithAddress(loginDto.Email);

    if (user == null) return Unauthorized(new ApiResponse(401));

    var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

    if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

    var clientUserLogin = _mapper.Map<ClientReturnRegisterDto>(user);

    clientUserLogin.Token = _serviceToken.CreateToken(user);

    return Ok(clientUserLogin);
  }

  [HttpPost("register")]
  public async Task<ActionResult<ClientReturnRegisterDto>> Register(ClientRegisterDto registerDto)
  {
    if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
    {
      return new BadRequestObjectResult(new ApiValidationErrorResponse
      { Errors = new[] { "Email address is in use" } });
    }

    var validator = _validatorClientRegisterDto.Validate(registerDto);

    if (!validator.IsValid)
      return BadRequest(new ApiResponse(400, validator.Errors.FirstOrDefault().ErrorMessage));


    var result = await _serviceClient.CreateClientAsync(registerDto);

    if (result.Error != null)  return BadRequest(new ApiResponse(400, validator.Errors.FirstOrDefault().ErrorMessage));

    return Ok(result.Data);

  }

  [HttpPut("update")]
  public async Task<ActionResult<ClientFullReturnDTO>> PutUpdate(ClientUpdateDTO dto)
  {

    var validator = _validatorClientUpdateDTO.Validate(dto);

    if (!validator.IsValid)
      return BadRequest(new ApiResponse(400, validator.Errors.FirstOrDefault().ErrorMessage));

    if (dto.Address != null)
    {
      if (dto.Address.Id == 0)
      {
        var addressCreate = _mapper.Map<AddressCreateDTO>(dto.Address);

        var validatorAddressCreateDTO = _validatorAddressCreateDTO.Validate(addressCreate);
        if (!validatorAddressCreateDTO.IsValid)
          return BadRequest(new ApiResponse(400, validatorAddressCreateDTO.Errors.FirstOrDefault().ErrorMessage));
      }
      else
      {
        var addressUpdate = _mapper.Map<AddressUpdateDTO>(dto.Address);

        var validatorAddressUpdateDTO = _validatorAddressUpdateDTO.Validate(addressUpdate);
        if (!validatorAddressUpdateDTO.IsValid)
          return BadRequest(new ApiResponse(400, validatorAddressUpdateDTO.Errors.FirstOrDefault().ErrorMessage));
      }
    }

    var result = await _serviceClient.UpdateClientAsync(dto);

    if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));

    return _mapper.Map<ClientFullReturnDTO>(result.Data);

  }

  [HttpGet("emailexists")]
  public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
  {
    return await _userManager.FindByEmailAsync(email) != null;
  }

  [Authorize]
  [HttpGet("address")]
  public async Task<ActionResult<AddressReturnDTO>> GetUserAddress()
  {
    var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

    return _mapper.Map<Address, AddressReturnDTO>(user.Address);
  }

  [Authorize]
  [HttpPut("address")]
  public async Task<ActionResult<AddressReturnDTO>> UpdateUserAddress(AddressUpdateDTO address)
  {
    var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

    user.Address = _mapper.Map<AddressUpdateDTO, Address>(address);

    var result = await _userManager.UpdateAsync(user);

    if (result.Succeeded) return Ok(_mapper.Map<AddressReturnDTO>(user.Address));

    return BadRequest("Problem updating the user");

  }
  [HttpGet("user-info")]
  public async Task<ActionResult> GetUserInfo()
  {
    if (User.Identity?.IsAuthenticated == false) return NoContent();

    var user = await _signInManager.UserManager.GetUserByEmailWithAddress(User);


    user.Address = new Address
    {
      Name = "Address1",
      City = "City1",
      State = "State",
      ZipCode = "123456",
      ClientId = user.Id,
    };

    return Ok(_mapper.Map<ClientReturnDTO>(user));
  }
}
