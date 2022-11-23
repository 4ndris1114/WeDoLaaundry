using System.Runtime.InteropServices;
using WebAppIdentity.Models;
namespace WebAppIdentity.ServiceLayer;

public interface ICustomerService
{
    Task<Customer> GetCustomerByUserId(string id);
    Task<bool> PostCustomer(Customer customer);
    Task<Customer>CreateCustomer(Customer customer);
    Task<bool>UpdateCustomer(Customer customer);
    Task<bool>DeleteCustomer(Customer customer);
}
