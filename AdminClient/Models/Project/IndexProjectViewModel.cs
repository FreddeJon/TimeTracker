namespace AdminClient.Models.Project;

public class IndexProjectsViewModel
{
    public List<ListProjectViewModel> Projects { get; set; }
    public int Limit { get; set; }
    public int TotalPage { get; set; }
    public int CurrentPage { get; set; }
    public int TotalProjects { get; set; }
    public CustomerDto Customer { get; set; }

    public class ListProjectViewModel
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
    }
}
