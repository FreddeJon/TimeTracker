// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace Domain.Entities;

public class Customer : EntityBase
{
    public Customer()
    {
        Projects = new HashSet<Project>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public HashSet<Project> Projects { get; set; }
}
