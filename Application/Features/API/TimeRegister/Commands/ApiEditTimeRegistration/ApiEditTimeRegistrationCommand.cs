using System.ComponentModel.DataAnnotations;

namespace Application.Features.API.TimeRegister.Commands.ApiEditTimeRegistration;
public class ApiEditTimeRegistrationCommand : IRequest<ApiEditTimeRegistrationResponse>
{
    public Guid CustomerId { get; }
    public Guid ProjectId { get; }
    public Guid TimeRegistrationId { get; }
    public EditTimeRegistrationModel Model { get; }

    public ApiEditTimeRegistrationCommand(Guid customerId, Guid projectId, Guid timeRegistrationId, EditTimeRegistrationModel model)
    {
        CustomerId = customerId;
        ProjectId = projectId;
        TimeRegistrationId = timeRegistrationId;
        Model = model;
    }


    public class EditTimeRegistrationModel
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
