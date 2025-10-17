using FluentValidation;

namespace BankAppTestBack.Application.UseCases.Account.Commands.UpdateAccount
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(v => v.AccountId)
                .GreaterThan(0).WithMessage("El ID de cuenta es obligatorio para la actualización.");

            When(v => !string.IsNullOrEmpty(v.Number), () =>
            {
                RuleFor(v => v.Number).NotEmpty().WithMessage("El número de cuenta no puede estar vacío.");
            });

            When(v => v.InitBalance.HasValue, () =>
            {
                RuleFor(v => v.InitBalance!.Value)
                    .GreaterThanOrEqualTo(0).WithMessage("El saldo inicial no puede ser negativo.");
            });

            When(v => v.ClientId.HasValue, () =>
            {
                RuleFor(v => v.ClientId!.Value)
                    .GreaterThan(0).WithMessage("El ID del nuevo cliente debe ser válido.");
            });
        }
    }
}
