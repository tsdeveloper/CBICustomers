using Core.DTOs.Addresses;
using Core.DTOs.Clients;
using FluentValidation;

namespace Core.Validators;

  public class AddressCreateDTOValidator : AbstractValidator<AddressCreateDTO>
{
  public AddressCreateDTOValidator()
  {
    RuleFor(c => c.ClientId).NotEmpty().NotNull().WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.Name).MaximumLength(200).WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.ZipCode).MaximumLength(8).WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.City).MaximumLength(150).WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.State).MaximumLength(2).WithMessage("{PropertyName} é obrigatório.");
  }
}

  public class AddressUpdateDTOValidator : AbstractValidator<AddressUpdateDTO>
{
  public AddressUpdateDTOValidator()
  {
    RuleFor(c => c.ClientId).NotEmpty().NotNull().WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.Name).MaximumLength(200).WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.ZipCode).MaximumLength(8).WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.City).MaximumLength(150).WithMessage("{PropertyName} é obrigatório.");
    RuleFor(c => c.State).MaximumLength(2).WithMessage("{PropertyName} é obrigatório.");
  }
}
