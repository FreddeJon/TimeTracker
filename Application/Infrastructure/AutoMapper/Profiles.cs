using Application.Features.API.Customers.Commands.ApiCreateCustomer;
using Application.Features.API.Customers.Commands.ApiEditCustomer;
using Application.Features.API.Customers.Query.ApiGetCustomerById;
using Application.Features.API.Customers.Query.ApiGetCustomers;

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



        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<TimeRegistration, TimeRegistrationDto>().ReverseMap();
    }
}


