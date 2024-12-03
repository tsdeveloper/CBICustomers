using Core.DTOs.Clients;
using FluentValidation;

namespace Core.Validators;

public class ClientRegisterDtoValidator : AbstractValidator<ClientRegisterDto>
{
  public ClientRegisterDtoValidator()
  {
    RuleFor(c => c.Name).NotEmpty().NotNull().MaximumLength(150).WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.Email).EmailAddress().NotEmpty().NotNull().WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.Password).NotEmpty().NotNull().WithMessage("{PropertyName} é obrigatório.");

  }
}
  public class ClientUpdateDTOValidator : AbstractValidator<ClientUpdateDTO>
{
  public ClientUpdateDTOValidator()
  {
    RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.Name).NotEmpty().NotNull().MaximumLength(150).WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.Phone).NotEmpty().NotNull().MaximumLength(11).WithMessage("{PropertyName} é obrigatório.");

  }
}
