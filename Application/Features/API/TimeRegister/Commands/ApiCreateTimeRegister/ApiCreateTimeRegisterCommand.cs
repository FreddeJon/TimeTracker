namespace Application.Features.API.TimeRegister.Commands.ApiCreateTimeRegister;
public class ApiCreateTimeRegisterCommand : IRequest<ApiCreateTimeRegisterResponse>
{
    public Guid CustomerId { get; }
    public Guid ProjectId { get; }
    public CreateTimeRegisterModel Model { get; }

    public ApiCreateTimeRegisterCommand(Guid customerId, Guid projectId, CreateTimeRegisterModel model)
    {
        CustomerId = customerId;
        ProjectId = projectId;
        Model = model;
    }

    public class CreateTimeRegisterModel
    {
        public DateTime Date { get; set; }
        public int TimeInMinutes { get; set; }
        public string Description { get; set; } = null!;
    }
}
