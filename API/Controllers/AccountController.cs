using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.DTOs.Addresses;
using Core.DTOs.Clients;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Interfaces.Services.Clients;
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
  public AccountController(UserManager<Client> userManager, SignInManager<Client> signInManager,
      ITokenService tokenService, IMapper mapper, IClientService serviceClient)
  {
    _mapper = mapper;
    _tokenService = tokenService;
    _signInManager = signInManager;
    _userManager = userManager;
    _serviceClient = serviceClient;
  }

  [Authorize]
  [HttpGet]
  public async Task<ActionResult<ClientReturnDTO>> GetCurrentUser()
  {
    var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

    return _mapper.Map<ClientReturnDTO>(user);
  }

  [HttpPost("login")]
  public async Task<ActionResult<ClientReturnDTO>> Login(ClientLoginDto loginDto)
  {
    var user = await _userManager.FindByEmailAsync(loginDto.Email);

    if (user == null) return Unauthorized(new ApiResponse(401));

    var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

    if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

    return _mapper.Map<ClientReturnDTO>(user);
  }

  [HttpPost("register")]
  public async Task<ActionResult<ClientReturnRegisterDto>> Register(ClientRegisterDto registerDto)
  {
    if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
    {
      return new BadRequestObjectResult(new ApiValidationErrorResponse
      { Errors = new[] { "Email address is in use" } });
    }

    var result = await _serviceClient.CreateClientAsync(registerDto);

    if (result.Error != null) return BadRequest(new ApiResponse(400, result.Error.Message));

    return result.Data;

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
}
