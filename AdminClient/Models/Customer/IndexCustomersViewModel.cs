namespace AdminClient.Models.Customer;
public class IndexCustomersViewModel
{
    public List<ListCustomerViewModel> Customers { get; set; }
    public int Limit { get; set; }
    public int TotalPage { get; set; }
    public int CurrentPage { get; set; }
    public int TotalCustomers { get; set; }

    public class ListCustomerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

