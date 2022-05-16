// ReSharper disable ClassNeverInstantiated.Global
namespace Domain.Entities;

public class Project : EntityBase
{
    public Guid Id { get; set; }
    public Customer Customer { get; set; } = null!;
    public string ProjectName { get; set; } = null!;
}
