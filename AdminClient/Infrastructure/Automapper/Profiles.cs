namespace AdminClient.Infrastructure.Automapper;
public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<CustomerDto, IndexCustomersViewModel.ListCustomerViewModel>().ReverseMap();
        CreateMap<CustomerDto, EditCustomerViewModel>().ReverseMap();



        CreateMap<ProjectDto, IndexProjectsViewModel.ListProjectViewModel>().ReverseMap();
        ;
    }
}
