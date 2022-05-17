using System.ComponentModel.DataAnnotations;

namespace AdminApp.Models.Customer;
public class CreateCustomerViewModel
{
    [MaxLength(40)]
    [Required]
    public string Name { get; set; }
}
