namespace AdminClient.DTO;
public class CustomerDto
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; } = null!;
}
