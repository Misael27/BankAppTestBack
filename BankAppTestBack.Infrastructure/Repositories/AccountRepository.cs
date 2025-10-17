using BankAppTestBack.Domain.Entities;
using BankAppTestBack.Domain.Repositories;
using Infrastructure.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BankAppTestBack.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Account account)
        {
            _context.Accounts.Add(account);
        }

        public async Task<Account?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Accounts.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<List<Account>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Accounts.ToListAsync(cancellationToken);
        }

        public void Remove(Account account)
        {
            _context.Accounts.Remove(account);
        }

        public async Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Accounts.AnyAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            return await _context.Accounts.AnyAsync(a => a.Number == number, cancellationToken);
        }
    }
}
