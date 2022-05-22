namespace AdminClient.Models.Project;
public class EditProjectViewModel
{
    [MaxLength(40, ErrorMessage = "Maximum length is 40")]
    [Required(ErrorMessage = "Project Name is required")]
    [JsonPropertyName("name")]
    public string ProjectName { get; set; }
}
