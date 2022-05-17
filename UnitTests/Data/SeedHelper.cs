using Domain.Entities;

namespace UnitTests.Data;
public static class SeedHelper
{
    public static void SeedCustomers(this TimeTrackerContext context)
    {
        var customers = new List<Customer>
        {
            new ()
            {
                Name = "Apple",
                Id = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39225"),
                Projects = new HashSet<Project>()
                {
                    new ()
                    {
                        Id = new Guid("4B2884EF-AFF2-4191-A186-EA5AA77A51FE"),
                        ProjectName = "Testing Something"
                    },
                    new ()
                    {
                        Id = new Guid("4B2884EF-AFF2-4191-A186-EA5AA77A51FD"),
                        ProjectName = "Testing Something Else"
                    }
                }
            },
            new ()
            {
                Name = "Microsoft",
                Id = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39226")
            },
            new ()
            {
                Name = "Google",
                Id = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39227")
            },
            new ()
            {
                Name = "Facebook",
                Id = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39228")
            },
            new ()
            {
                Name = "Klarna",
                Id = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39229")
            },
            new ()
            {
                Name = "Spotify",
                Id = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39215")
            },
            new ()
            {
                Name = "Swedbank",
                Id = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39235")
            },
            new ()
            {
                Name = "Aftonbladet",
                Id = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39245")
            },
            new ()
            {
                Name = "Netflix",
                Id = new Guid("5783CEE0-77EF-42C9-94A5-97EC06C39255")
            }
        };

        context.Customers.AddRange(customers);
        context.SaveChanges();
    }

}
