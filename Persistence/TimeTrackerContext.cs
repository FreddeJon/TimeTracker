using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

#pragma warning disable CS8618
namespace Persistence;

public class TimeTrackerContext : IdentityDbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<TimeRegistration> TimeRegistrations { get; set; }


    public TimeTrackerContext(DbContextOptions<TimeTrackerContext> options) : base(options)
    {

    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.LastModifiedDateUtc = DateTime.UtcNow;
                    break;
                case EntityState.Added:
                    entry.Entity.CreatedDateUtc = DateTime.UtcNow;
                    entry.Entity.LastModifiedDateUtc = DateTime.UtcNow;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.LastModifiedDateUtc = DateTime.UtcNow;
                    break;
                case EntityState.Added:
                    entry.Entity.CreatedDateUtc = DateTime.UtcNow;
                    entry.Entity.LastModifiedDateUtc = DateTime.UtcNow;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
