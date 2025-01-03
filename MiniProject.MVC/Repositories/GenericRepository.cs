using Microsoft.EntityFrameworkCore;
using MiniProject.MVC.Data;
using MiniProject.MVC.Models.Base;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MiniProject.MVC.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
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
        public async Task<T> GetByIdAsync(int id, List<string> includes = null)
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

            return await query.FirstOrDefaultAsync(x=>x.Id == id);
        }

        // Find by condition
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        // Add a new entity
        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
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


        // Save changes to the database
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool Exit(int Id)
        {
            IQueryable<T> query = _dbSet;
            return _dbSet.Any(_dbSet => _dbSet.Id == Id);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
