using System.ComponentModel.DataAnnotations;

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
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Range(1, 1440)]
        public int TimeInMinutes { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = null!;
    }
}
