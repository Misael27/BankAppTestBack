using BankAppTestBack.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using BankAppTestBack.Domain.Entities;
using Infrastructure.Infrastructure;
using BankAppTestBack.Domain.Projections;

namespace BankAppTestBack.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly DataContext _context;

        public ReportRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MovementReport>> GetMovementReportAsync(
            long clientId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default)
        {
            var normalizedEndDate = endDate.Date.AddDays(1).AddSeconds(-1);
            var normalizedStartDate = startDate.Date;

            return await _context.Movements
                .Where(m => m.Date.Date >= normalizedStartDate && m.Date.Date <= normalizedEndDate
                            && m.Account.ClientId == clientId)
                .Select(m => new MovementReport(
                    m.Id,
                    m.Date,
                    m.Account.Client.Name,
                    m.Account.Number,
                    m.Account.Type,
                    m.Account.State,
                    m.Type == EMovementType.Retiro
                                ? m.Balance + m.Value
                                : m.Balance - m.Value,
                    m.Type == EMovementType.Retiro
                                ? m.Value * -1
                                : m.Value,
                    m.Balance
                ))
                .ToListAsync(cancellationToken);
        }
    }
}