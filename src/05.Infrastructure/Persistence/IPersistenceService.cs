using CRUD.ManagementUser.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD.ManagementUser.Infrastructure.Persistence
{
    public interface IPersistenceService
    {
        DbSet<Employee> Employees { get; }
        Task<int> SaveChangesAsync<THandler>(THandler handler, CancellationToken cancellationToken = default) where THandler : notnull;
    }
}