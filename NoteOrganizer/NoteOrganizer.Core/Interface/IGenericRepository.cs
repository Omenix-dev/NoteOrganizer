

namespace NoteOrganizer.Core.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task DeleteAsync(string id);
        void DeleteRangeAsync(IEnumerable<T> entities);
        IQueryable<T> GetAllAsync();
        Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression, List<string> includes = null);
        Task InsertAsync(T entity);
        void Update(T item);
    }
}
