using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Application.UseCases.Account.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(v => v.Number).NotEmpty().WithMessage("El número de cuenta es requerido.");

            RuleFor(v => v.Type).IsInEnum().WithMessage("El tipo de cuenta es inválido.");

            RuleFor(v => v.InitBalance)
                .GreaterThanOrEqualTo(0).WithMessage("El saldo inicial no puede ser negativo.");

            RuleFor(v => v.ClientId)
                .GreaterThan(0).WithMessage("El ID del cliente es obligatorio.");
        }
    }
}
