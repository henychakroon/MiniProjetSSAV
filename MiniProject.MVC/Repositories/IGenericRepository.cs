using System.Linq.Expressions;

namespace MiniProject.MVC.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        // Get all records with optional includes and predicates
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            List<string> includes = null);

        // Get a record by ID
        Task<T> GetByIdAsync(object id);

        // Find records by condition
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Add a new entity
        Task AddAsync(T entity);

        // Update an existing entity
        void Update(T entity);

        // Delete an entity
        void Delete(T entity);

        // Delete an entity by ID
        Task DeleteByIdAsync(object id);

        // Save changes to the database
        Task SaveAsync();
    }
}
