﻿
using CustomerService.Domain;

namespace CustomerService.Application.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customer { get; }
        IRepository<CustomerAddress> CustomerAddress { get; }
        Task SaveChangesAsync();
    }
}