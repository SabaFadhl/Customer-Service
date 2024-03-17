using CustomerService.Application.Interface;

namespace CustomerService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MasterContext _context;
        private bool _disposed;

        public UnitOfWork(MasterContext context)
        {
            _context = context;
            _disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
