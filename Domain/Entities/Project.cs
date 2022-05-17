// ReSharper disable ClassNeverInstantiated.Global
namespace Domain.Entities;

public class Project : EntityBase
{
    public Project()
    {
        TimeRegistrations = new HashSet<TimeRegistration>();
    }
    public Guid Id { get; set; }
    public Customer Customer { get; set; } = null!;
    public string ProjectName { get; set; } = null!;
    public HashSet<TimeRegistration> TimeRegistrations { get; set; }
}
