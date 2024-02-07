using FluentValidation;
using ItPro.Api.Models;

namespace ItPro.Api.Validators;

/// <summary>
/// Валидатор для клиента, получаемого из запроса к API.
/// </summary>
public sealed class ClientModelValidator : AbstractValidator<ClientModel>
{
    public ClientModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Не задано имя клиента.");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("Не задано имя клиента.");

        RuleFor(x => x.BirthDay)
            .NotEmpty()
            .WithMessage("Не задана дата рождения.");
    }
}