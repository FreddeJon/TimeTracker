namespace Application.DTO;
public class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public HashSet<ProjectDto> Projects { get; set; }
    public HashSet<TimeRegistrationDto> TimeRegistrations { get; set; }
}
