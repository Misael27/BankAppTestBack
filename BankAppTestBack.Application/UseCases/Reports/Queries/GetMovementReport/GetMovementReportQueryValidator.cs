using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Application.UseCases.Reports.Queries.GetMovementReport
{
    public class GetMovementReportQueryValidator : AbstractValidator<GetMovementReportQuery>
    {
        public GetMovementReportQueryValidator()
        {
            RuleFor(x => x.ClientId)
                .GreaterThan(0)
                .WithMessage("CLIENTID_REQUIRED: El ID del cliente debe ser un valor positivo.");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("STARTDATE_REQUIRED: La fecha de inicio es requerida.");

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("ENDDATE_REQUIRED: La fecha de fin es requerida.");

            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate)
                .When(x => x.StartDate != default && x.EndDate != default)
                .WithMessage("DATE_RANGE_INVALID: La fecha de inicio debe ser anterior o igual a la fecha de fin.");
        }
    }
}
