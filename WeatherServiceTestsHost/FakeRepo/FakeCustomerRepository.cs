using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsWithHost.FakeRepo;

// demo of the basic technique
// of an "in memory fake database"
// a lightwitght way to fake a dependency

public record Customer(int Id, DateOnly DateOfBirth, string Name);

public interface ICustomerRepository
{
    public Customer? Get(int id);
    public void Add(Customer customer);
}

public class FakeCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _data = new();

    public void Add(Customer customer)
        => _data.Add(customer);

    public Customer? Get(int id) 
        => _data.FirstOrDefault(c => c.Id == id);

    /// <summary>
    /// Non-intwerfaced methods can be used in test 
    /// To verify that the expected dta has been sent to the data store
    /// </summary>
    /// <returns></returns>
    public IReadOnlyCollection<Customer> GetAll()
        => _data;
}
