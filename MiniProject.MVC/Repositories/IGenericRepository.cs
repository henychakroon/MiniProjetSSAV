using MiniProject.MVC.Models.Base;
using System.Linq.Expressions;

namespace MiniProject.MVC.Repositories
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        // Get all records with optional includes and predicates
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            List<string> includes = null);

        // Get a record by ID
        Task<T> GetByIdAsync(int id, List<string> includes = null);

        // Find records by condition
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        bool Exit(int Id);
        // Add a new entity
        void Add(T entity);

        // Update an existing entity
        void Update(T entity);

        // Delete an entity
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        // Save changes to the database
        Task SaveAsync();
    }
}
