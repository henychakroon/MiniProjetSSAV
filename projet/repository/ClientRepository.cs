using Microsoft.EntityFrameworkCore;
using projet.Models;

namespace projet.repository
{
    // Repositories/ClientRepository.cs
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync() => await _context.Clients.ToListAsync();
        public async Task<Client> GetByIdAsync(int id) => await _context.Clients.FindAsync(id);
        public async Task AddAsync(Client client) { await _context.Clients.AddAsync(client); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Client client) { _context.Clients.Update(client); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var client = await _context.Clients.FindAsync(id); if (client != null) { _context.Clients.Remove(client); await _context.SaveChangesAsync(); } }
    }

}
