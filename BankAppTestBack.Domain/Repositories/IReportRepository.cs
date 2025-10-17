using BankAppTestBack.Domain.Projections;

namespace BankAppTestBack.Domain.Abstractions
{
    public interface IReportRepository
    {
        Task<List<MovementReport>> GetMovementReportAsync(
            long clientId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default);
    }
}