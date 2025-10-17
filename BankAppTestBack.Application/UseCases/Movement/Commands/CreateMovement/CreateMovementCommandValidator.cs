using FluentValidation;

namespace BankAppTestBack.Application.UseCases.Movement.Commands.CreateMovement
{
    public class CreateMovementCommandValidator : AbstractValidator<CreateMovementCommand>
    {
        public CreateMovementCommandValidator()
        {
            RuleFor(v => v.Type).IsInEnum().WithMessage("El tipo de movimiento es inválido.");

            RuleFor(v => v.Value)
                .GreaterThan(0).WithMessage("El valor del movimiento debe ser positivo.");

            RuleFor(v => v.AccountId)
                .GreaterThan(0).WithMessage("El ID de cuenta es obligatorio.");
        }
    }
}