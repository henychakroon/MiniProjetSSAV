namespace projet.repository
{
    // Repositories/InterventionRepository.cs
    using Microsoft.EntityFrameworkCore;
    using projet.Data;
    using projet.Models;

    public class InterventionRepository : IInterventionRepository
    {
        private readonly ApplicationDbContext _context;

        public InterventionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Intervention>> GetAllAsync() => await _context.Interventions.ToListAsync();

        public async Task<Intervention> GetByIdAsync(int id) => await _context.Interventions.FindAsync(id);

        public async Task AddAsync(Intervention intervention)
        {
            await _context.Interventions.AddAsync(intervention);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Intervention intervention)
        {
            _context.Interventions.Update(intervention);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention != null)
            {
                _context.Interventions.Remove(intervention);
                await _context.SaveChangesAsync();
            }
        }
    }

}
