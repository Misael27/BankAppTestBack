using BankAppTestBack.Domain.Entities;


namespace BankAppTestBack.Domain.Repositories
{
    public interface IAccountRepository
    {
        void Add(Account account);
        Task<Account?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        void Remove(Account account);
        Task<List<Account>> FindAllAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNumberAsync(string number, CancellationToken cancellationToken = default);
    }
}
