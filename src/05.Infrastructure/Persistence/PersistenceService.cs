using CRUD.ManagementUser.Application.Services.CurrentUser;
using CRUD.ManagementUser.Application.Services.DateAndTime;
using CRUD.ManagementUser.Application.Services.DomainEvent;
using CRUD.ManagementUser.Application.Services.Persistence;
using CRUD.ManagementUser.Domain.Entities;
using CRUD.ManagementUser.Domain.Events;
using CRUD.ManagementUser.Domain.Interfaces;
using CRUD.ManagementUser.Infrastructure.Persistence.Common.Extensions;
using CRUD.ManagementUser.Infrastructure.Persistence.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Emit;

namespace CRUD.ManagementUser.Infrastructure.Persistence;

//public partial class PersistenceService : IdentityDbContext<ApplicationUser>
public partial class PersistenceService : IdentityDbContext
{
    private const string SoftDeleted = nameof(SoftDeleted);

    protected ICurrentUserService _currentUser = default!;
    protected IDateAndTimeService _dateTime = default!;
    protected IDomainEventService _domainEvent = default!;

    public PersistenceService(
        DbContextOptions<PersistenceService> options,
        ICurrentUserService currentUser,
        IDateAndTimeService dateTime,
        IDomainEventService domainEvent) : base(options)
    {
        _currentUser = currentUser;
        _dateTime = dateTime;
        _domainEvent = domainEvent;
    }

    protected PersistenceService(DbContextOptions options)
        : base(options)
    {
    }

    public async Task<int> SaveChangesAsync<THandler>(THandler handler, CancellationToken cancellationToken = default) where THandler : notnull
    {
        foreach (var entry in ChangeTracker.Entries<ICreatable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUser.Username;
                    entry.Entity.Created = _dateTime.Now;
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IModifiable>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = _currentUser.Username;
                    entry.Entity.Modified = _dateTime.Now;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(true, cancellationToken);

        await DispatchEvents();

        return result;
    }

    private async Task DispatchEvents()
    {
        while (true)
        {
            var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .FirstOrDefault();

            if (domainEventEntity is null)
            {
                break;
            }

            domainEventEntity.IsPublished = true;

            await _domainEvent.Publish(domainEventEntity);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromNameSpace(typeof(EmployeeConfiguration).Namespace!);

        var schema = "Identity";

        builder.Entity<IdentityUser>(b =>
        {
            b.ToTable(nameof(IdentityUser), schema);
        });

        builder.Entity<IdentityUserClaim<string>>(b =>
        {
            b.ToTable(nameof(IdentityUserClaim<string>), schema);
        });

        builder.Entity<IdentityUserLogin<string>>(b =>
        {
            b.ToTable(nameof(IdentityUserLogin<string>), schema);
        });

        builder.Entity<IdentityUserToken<string>>(b =>
        {
            b.ToTable(nameof(IdentityUserToken<string>), schema);
        });

        builder.Entity<IdentityRole>(b =>
        {
            b.ToTable(nameof(IdentityRole), schema);
        });

        builder.Entity<IdentityRoleClaim<string>>(b =>
        {
            b.ToTable(nameof(IdentityRoleClaim<string>), schema);
        });

        builder.Entity<IdentityUserRole<string>>(b =>
        {
            b.ToTable(nameof(IdentityUserRole<string>), schema);
        });
    }
}
