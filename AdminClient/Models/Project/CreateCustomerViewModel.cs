namespace AdminClient.Models.Project;
public class CreateProjectViewModel
{
    public string? CustomerName { get; set; }


    [MaxLength(40, ErrorMessage = "Maximum length is 40")]
    [Required(ErrorMessage = "Project Name is required")]
    public string ProjectName { get; set; } = null!;
}
