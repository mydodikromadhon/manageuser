using CRUD.ManagementUser.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD.ManagementUser.Infrastructure.Persistence;

public partial class PersistenceService : IPersistenceService
{
    public DbSet<Employee> Employees => Set<Employee>();
}
