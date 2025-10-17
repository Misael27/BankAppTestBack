using BankAppTestBack.Domain.Entities;

namespace BankAppTestBack.Domain.Repositories
{
    public interface IMovementRepository
    {
        void Add(Movement movement);
        Task<Movement?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        void Remove(Movement movement);
        Task<List<Movement>> FindAllAsync(CancellationToken cancellationToken = default);
        Task<Movement?> FindLastMovementByAccountIdAsync(long accountId, CancellationToken cancellationToken = default);
        Task<double> GetTodayDebitTotalAsync(long accountId, CancellationToken cancellationToken = default);
    }
}