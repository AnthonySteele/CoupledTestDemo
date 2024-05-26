namespace TestsWithHost.FakeRepo;

// demo of the basic technique
// of an "in memory fake database"
// a lightwitght way to fake a dependency
// the type and interface defintions are stubs
// to show how the fake repository would work

public record Customer(int Id, DateOnly DateOfBirth, string Name, string Address, bool active);

/// <summary>
/// Define some basic Create, Read, Update operations on the customer type
/// </summary>
public interface ICustomerRepository
{
    public void Add(Customer customer);
    public Customer? Get(int id);
    public bool Activate(int id);
}

public class FakeCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _data = new();

    public void Add(Customer customer)
        => _data.Add(customer);

    public Customer? Get(int id) 
        => _data.SingleOrDefault(c => c.Id == id);

    public bool Activate(int id)
    {
        var matchingCustomer = Get(id);
        if (matchingCustomer != null)
        {
            var update = matchingCustomer with { active = true };
            _data.Remove(matchingCustomer);
            _data.Add(update);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Non-interfaced methods can be used in tests 
    /// To verify that the expected data is in the data store
    /// </summary>
    /// <returns></returns>
    public IReadOnlyCollection<Customer> GetAll()
        => _data;

    public bool Any(Func<Customer, bool> predicate)
        => _data.Any(predicate);

}
