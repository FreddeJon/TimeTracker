using Application.Features.Customer.Query.GetCustomersWithProjectsPaginated;

namespace Application.Infrastructure.AutoMapper;
public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<TimeRegistration, TimeRegistrationDto>().ReverseMap();
    }
}


