using CustomerService.Application.Interface;
using CustomerService.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CustomerService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MasterContext _context;
        private bool _disposed;

        public IRepository<Customer> Customer { get; private set; }

        public UnitOfWork(MasterContext context)
        {
            _context = context;
            _disposed = false;
            Customer = new Repository<Customer>(_context);
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
