#pragma warning disable CS8618
namespace Persistence;

public class TimeTrackerContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Project> Projects { get; set; }


    public TimeTrackerContext(DbContextOptions<TimeTrackerContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
