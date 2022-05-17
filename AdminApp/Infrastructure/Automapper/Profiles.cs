namespace AdminApp.Infrastructure.AutoMapper;
public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<CustomerDto, CreateCustomerViewModel>().ReverseMap();
        CreateMap<CustomerDto, IndexCustomersViewModel.ListCustomerViewModel>().ReverseMap();
        CreateMap<CustomerDto, EditCustomerViewModel>().ReverseMap();
    }
}
