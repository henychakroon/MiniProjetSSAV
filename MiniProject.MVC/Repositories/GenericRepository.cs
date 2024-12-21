using Microsoft.EntityFrameworkCore;
using MiniProject.MVC.Data;
using System.Linq.Expressions;

namespace MiniProject.MVC.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        // Get all records with optional includes and predicates
        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            // Apply includes
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Apply predicate
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        // Get by ID
        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Find by condition
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        // Add a new entity
        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
        }

        // Update an existing entity
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        // Delete by entity
        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
        }

        // Delete by ID
        public async Task DeleteByIdAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        // Save changes to the database
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
