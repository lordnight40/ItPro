using FluentValidation;
using ItPro.Api.Models;

namespace ItPro.Api.Validators;

public sealed class OrderModelValidator : AbstractValidator<OrderModel>
{
    public OrderModelValidator()
    {
        RuleFor(model => model.Amount)
            .NotNull()
            .WithMessage("Не указана стоимость.")
            .GreaterThanOrEqualTo(0m)
            .WithMessage("Стоимость не может быть отрицательной.");

        RuleFor(model => model.CreatedAt)
            .NotEmpty()
            .WithMessage("Не указана дата создания заказа.");

        RuleFor(model => model.ClientId)
            .NotEmpty()
            .WithMessage("Не указан клиент.");
    }
}