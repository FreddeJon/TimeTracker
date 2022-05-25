
namespace AdminClient.Models.Customer;
public class EditCustomerViewModel
{
    [MaxLength(40, ErrorMessage = "Maximum length is 40")]
    [Required(ErrorMessage = "Customer Name is required")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
}
