namespace projet.repository
{
    // Repositories/ComplaintRepository.cs
    using Microsoft.EntityFrameworkCore;
    using projet.Data;
    using projet.Models;

    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Complaint>> GetAllAsync() => await _context.Complaints.ToListAsync();

        public async Task<Complaint> GetByIdAsync(int id) => await _context.Complaints.FindAsync(id);

        public async Task AddAsync(Complaint complaint)
        {
            await _context.Complaints.AddAsync(complaint);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Complaint complaint)
        {
            _context.Complaints.Update(complaint);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
                await _context.SaveChangesAsync();
            }
        }
    }

}
