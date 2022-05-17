using Application.Features.API.Customers.Commands.ApiCreateCustomer;
using Application.Features.API.Customers.Commands.ApiEditCustomer;
using Application.Features.API.Customers.Query.ApiGetCustomerById;
using Application.Features.API.Customers.Query.ApiGetCustomers;
using Application.Features.API.Projects.Commands.ApiCreateProject;
using Application.Features.API.Projects.Commands.ApiEditProject;
using Application.Features.API.Projects.Query.ApiGetProjectById;
using Application.Features.API.Projects.Query.ApiGetProjects;

namespace Application.Infrastructure.AutoMapper;
public class Profiles : Profile
{
    public Profiles()
    {
        //API GetCustomers
        CreateMap<Customer, ApiGetCustomersPaginatedQueryHandler.CustomerDto>().ReverseMap();


        // API GetCustomerById
        CreateMap<Customer, ApiGetCustomerByIdQueryHandler.CustomerDto>().ReverseMap();

        // API CreateCustomer
        CreateMap<Customer, ApiCreateCustomerCommandHandler.CustomerDto>().ReverseMap();

        // API EditCustomer
        CreateMap<Customer, ApiEditCustomerCommandHandler.CustomerDto>().ReverseMap();



        // API GetProjects
        CreateMap<Project, ApiGetProjectsForCustomerPaginatedQueryHandler.ProjectDto>().ReverseMap();

        // API GetProjectById
        CreateMap<Project, ApiGetProjectByIdQueryHandler.ProjectDto>().ReverseMap();

        // API CreateProject
        CreateMap<Project, ApiCreateProjectCommandHandler.ProjectDto>().ReverseMap();
        
        
        // API EditProject
        CreateMap<Project, ApiEditProjectCommandHandler.ProjectDto>().ReverseMap();



        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<TimeRegistration, TimeRegistrationDto>().ReverseMap();
    }
}


