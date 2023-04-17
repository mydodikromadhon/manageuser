using CRUD.ManagementUser.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD.ManagementUser.Application.Services.Persistence;

public interface IPersistenceService
{
    #region Essential Entities
    #endregion Essential Entities

    #region Business Entities
    DbSet<Employee> Employees { get; }
    #endregion Business Entities

    Task<int> SaveChangesAsync<THandler>(THandler handler, CancellationToken cancellationToken = default) where THandler : notnull;
}
