using DockerComposeRiderIssue.Infrastructure.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DockerComposeRiderIssue.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<BusinessCentralInstance> BusinessCentralInstances { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public override int SaveChanges()
    {
        UpdateBaseEntity();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateBaseEntity();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateBaseEntity();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        UpdateBaseEntity();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateBaseEntity()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e is { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified });

        foreach (EntityEntry entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity) entityEntry.Entity).CreatedAt = DateTime.UtcNow;
            }

            ((BaseEntity) entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
        }
    }
}