using BankAppTestBack.Domain.Entities;
using BankAppTestBack.Domain.Repositories;
using Infrastructure.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BankAppTestBack.Infrastructure.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly DataContext _context;

        public MovementRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Movement movement)
        {
            _context.Movements.Add(movement);
        }

        public async Task<Movement?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Movements.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<List<Movement>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Movements.ToListAsync(cancellationToken);
        }

        public void Remove(Movement movement)
        {
            _context.Movements.Remove(movement);
        }

        public async Task<Movement?> FindLastMovementByAccountIdAsync(long accountId, CancellationToken cancellationToken = default)
        {
            return await _context.Movements
                .Where(m => m.AccountId == accountId)
                .OrderByDescending(m => m.Date)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<double> GetTodayDebitTotalAsync(long accountId, CancellationToken cancellationToken = default)
        {
            DateTime today = DateTime.UtcNow.Date;

            return await _context.Movements
                .Where(m => m.AccountId == accountId &&
                            m.Type == EMovementType.Retiro &&
                            m.Date.Date == today)
                .SumAsync(m => m.Value, cancellationToken);
        }
    }
}