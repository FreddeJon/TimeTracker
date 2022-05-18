using Application.Features.API.Customers.Commands.ApiCreateCustomer;
using Application.Features.API.Customers.Commands.ApiEditCustomer;
using Application.Features.API.Customers.Query.ApiGetCustomerById;
using Application.Features.API.Customers.Query.ApiGetCustomers;
using Application.Features.API.Projects.Commands.ApiCreateProject;
using Application.Features.API.Projects.Commands.ApiEditProject;
using Application.Features.API.Projects.Query.ApiGetProjectById;
using Application.Features.API.Projects.Query.ApiGetProjects;
using Application.Features.API.TimeRegister.Commands.ApiCreateTimeRegister;
using Application.Features.API.TimeRegister.Commands.ApiEditTimeRegistration;
using Application.Features.API.TimeRegister.Query.ApiGetRegisterById;
using Application.Features.API.TimeRegister.Query.ApiGetTimeRegistersPaginated;

namespace Application.Infrastructure.AutoMapper;
public class ApiProfiles : Profile
{
    public ApiProfiles()
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


        // API GetRegisters
        CreateMap<TimeRegistration, ApiGetTimeRegistersForProjectPaginatedQueryHandler.TimeRegistrationDto>()
            .ReverseMap();

        // API CreateTimeRegistration
        CreateMap<TimeRegistration, ApiCreateTimeRegisterCommand.CreateTimeRegisterModel>().ReverseMap();
        CreateMap<TimeRegistration, ApiCreateTimeRegisterCommandHandler.TimeRegistrationDto>().ReverseMap();


        // API GetTimeRegisterById
        CreateMap<TimeRegistration, ApiGetTimeRegistrationByIdQueryHandler.TimeRegistrationDto>().ReverseMap();


        // API EditTimeRegister
        CreateMap<TimeRegistration, ApiEditTimeRegistrationCommandHandler.TimeRegistrationDto>().ReverseMap();
    }
}
