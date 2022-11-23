using System.Runtime.InteropServices;
using WebAppIdentity.Models;
namespace WebAppIdentity.ServiceLayer;

public interface ICustomerService
{
   Task<bool> PostCustomer(Customer customer);
    Customer GetCustomerById(int id);
    int CreateCustomer(Customer customer);
    bool UpdateCustomer(Customer customer);
    bool DeleteCustomer(Customer customer);
}
