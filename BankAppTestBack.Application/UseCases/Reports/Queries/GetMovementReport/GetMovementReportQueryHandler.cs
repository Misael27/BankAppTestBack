using MediatR;
using BankAppTestBack.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BankAppTestBack.Domain.Projections;

namespace BankAppTestBack.Application.UseCases.Reports.Queries.GetMovementReport
{
    public class GetMovementReportQueryHandler : IRequestHandler<GetMovementReportQuery, List<MovementReport>>
    {
        private readonly IReportRepository _reportRepository;

        public GetMovementReportQueryHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<List<MovementReport>> Handle(GetMovementReportQuery request, CancellationToken cancellationToken)
        {
            return await _reportRepository.GetMovementReportAsync(
                request.ClientId,
                request.StartDate,
                request.EndDate,
                cancellationToken
            );
        }
    }
}