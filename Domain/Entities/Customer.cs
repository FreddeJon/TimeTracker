// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace Domain.Entities;

public class Customer : EntityBase
{
    public Customer()
    {
        Projects = new HashSet<Project>();
        TimeRegistrations = new HashSet<TimeRegistration>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public HashSet<Project> Projects { get; set; }
    public HashSet<TimeRegistration> TimeRegistrations { get; set; }
}
