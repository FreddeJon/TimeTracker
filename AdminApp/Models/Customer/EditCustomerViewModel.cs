
namespace AdminApp.Models.Customer;
public class EditCustomerViewModel
{
    [MaxLength(40)]
    [Required]
    public string Name { get; set; }
}
