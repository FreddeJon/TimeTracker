namespace AdminClient.Models.Customer;
public class CreateCustomerViewModel
{
    [MaxLength(40, ErrorMessage = "Maximum length is 40")]
    [Required(ErrorMessage = "Customer Name is required")]
    public string Name { get; set; }
}
