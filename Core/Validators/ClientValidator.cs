using Core.DTOs.Clients;
using FluentValidation;

namespace Core.Validators;

public class ClientRegisterDtoValidator : AbstractValidator<ClientRegisterDto>
{
  public ClientRegisterDtoValidator()
  {
    RuleFor(c => c.DisplayName).NotEmpty().NotNull().WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.Email).EmailAddress().NotEmpty().NotNull().WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.Password).NotEmpty().NotNull().WithMessage("{PropertyName} é obrigatório.");

  }
}
