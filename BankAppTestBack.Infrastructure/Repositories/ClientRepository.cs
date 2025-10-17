using BankAppTestBack.Domain.Entities;
using BankAppTestBack.Domain.Repositories;
using Infrastructure.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Client client)
        {
            _context.Clients.Add(client);
        }

        public async Task<bool> ExistsByPersonIdAsync(string personId, CancellationToken cancellationToken = default)
        {
            return await _context.Clients
                .AnyAsync(c => c.PersonId == personId, cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Clients.AnyAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Client?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Clients.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<List<Client>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Clients
                .ToListAsync(cancellationToken);
        }

        public void Remove(Client client)
        {
            _context.Clients.Remove(client);
        }
    }
}
