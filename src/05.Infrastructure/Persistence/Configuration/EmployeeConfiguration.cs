using CRUD.ManagementUser.Application.Services.Persistence;
using CRUD.ManagementUser.Domain.Entities;
using CRUD.ManagementUser.Infrastructure.Persistence.Common.Constants;
using CRUD.ManagementUser.Infrastructure.Persistence.Common.Extensions;
using CRUD.ManagementUser.Application.Employees.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD.ManagementUser.Infrastructure.Persistence.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(nameof(IPersistenceService.Employees), nameof(CRUD.ManagementUser));
        builder.ConfigureCreatableProperties();
        builder.ConfigureModifiableProperties();

        builder.Property(e => e.Number).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Number));
        builder.Property(e => e.FullName).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.FullName));
    }
}
