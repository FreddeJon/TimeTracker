namespace AdminApp.Models.Customer;
public class IndexCustomersViewModel
{
    public List<ListCustomerViewModel> Customers { get; set; }

    public class ListCustomerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
