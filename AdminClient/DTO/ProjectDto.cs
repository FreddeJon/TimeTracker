namespace AdminClient.DTO;
public class ProjectDto
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("projectName")] public string ProjectName { get; set; } = null!;
}
