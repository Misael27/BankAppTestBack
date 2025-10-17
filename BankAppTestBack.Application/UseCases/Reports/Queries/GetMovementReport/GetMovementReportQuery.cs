using MediatR;
using System;
using System.Collections.Generic;
using BankAppTestBack.Domain.Projections;

namespace BankAppTestBack.Application.UseCases.Reports.Queries.GetMovementReport
{
    public record GetMovementReportQuery(
        long ClientId,
        DateTime StartDate,
        DateTime EndDate
    ) : IRequest<List<MovementReport>>;
}
