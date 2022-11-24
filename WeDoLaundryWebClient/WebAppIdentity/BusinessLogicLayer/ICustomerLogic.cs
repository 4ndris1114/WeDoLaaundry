using WebAppIdentity.Models;

namespace WebAppIdentity.BusinessLogicLayer
{
    public interface ICustomerLogic
    {
      Task<Customer> GetCustomerByUserId(string id);
      Task<bool> InsertCustomer(Customer customer);
      //Task<Customer>CreateCustomer(Customer customer);
      Task<bool>UpdateCustomer(Customer customer);
      Task<bool>DeleteCustomer(Customer customer);
    }
}

