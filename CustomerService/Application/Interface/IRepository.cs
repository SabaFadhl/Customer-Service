using CustomerService.Domain;
using System.Linq.Expressions;

namespace CustomerService.Application.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        Task<TEntity> GetById(string id);
        Task<TEntity> GetByIdAsync(int id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> AnyAsync();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        //void RemoveById(int id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        int Count();
        bool Any(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetPaginated(int page, int pageSize);
        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> OrderBy(Expression<Func<TEntity, object>> orderByExpression);
        IEnumerable<IGrouping<TKey, TEntity>> GroupBy<TKey>(Expression<Func<TEntity, TKey>> groupByExpression);
        bool SaveChanges();
        IEnumerable<TEntity> ExecuteSqlQuery(string sql, params object[] parameters);
        void AddRange(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Attach(TEntity entity);
        void Detach(TEntity entity);
        void Dispose();
       
        Task<List<TEntity>> GetAllPageing(int pageIndex, int pageSize);
    }
}
