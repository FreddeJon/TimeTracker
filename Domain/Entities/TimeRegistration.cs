// ReSharper disable ClassNeverInstantiated.Global
namespace Domain.Entities;
public class TimeRegistration : EntityBase
{
    public Guid Id { get; set; }
    public Project Project { get; set; } = null!;
    public DateTime Date { get; set; }
    public int TimeInMinutes { get; set; }
    public string Description { get; set; } = null!;
}
