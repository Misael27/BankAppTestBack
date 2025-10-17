using BankAppTestBack.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Application.UseCases.Client.Commands.UpdateClient
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator(IClientRepository clientRepository)
        {
            RuleFor(v => v.ClientId)
                .GreaterThan(0).WithMessage("El ID del cliente es obligatorio.");

            When(v => !string.IsNullOrEmpty(v.Name), () =>
            {
                RuleFor(v => v.Name).NotEmpty().WithMessage("El nombre no puede estar vacío.");
            });

            When(v => !string.IsNullOrEmpty(v.PersonId), () =>
            {
                RuleFor(v => v.PersonId).NotEmpty().WithMessage("El ID de persona no puede estar vacío.");
            });

            When(v => v.Birthdate.HasValue, () =>
            {
                RuleFor(v => v.Birthdate!.Value)
                    .Must(BeBeforeToday).WithMessage("La fecha de nacimiento no puede ser futura.");
            });
        }

        private bool BeBeforeToday(DateTime date)
        {
            return date.Date < DateTime.UtcNow.Date;
        }
    }
}
