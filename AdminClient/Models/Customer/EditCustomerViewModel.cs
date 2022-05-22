
namespace AdminClient.Models.Customer;
public class EditCustomerViewModel
{
    [MaxLength(40)]
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
