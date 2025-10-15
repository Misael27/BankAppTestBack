using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<Domain.Entities.Client?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        void Add(Domain.Entities.Client client);
        void Remove(Domain.Entities.Client client);

        Task<bool> ExistsByPersonIdAsync(string personId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<List<Domain.Entities.Client>> FindAllAsync(CancellationToken cancellationToken = default);
    }
}
