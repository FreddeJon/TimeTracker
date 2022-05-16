namespace Application.DTO;

public class TimeRegistrationDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int TimeInMinutes { get; set; }
    public string Description { get; set; } = null!;
}