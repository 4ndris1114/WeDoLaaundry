using WebAppIdentity.Models;

namespace WebAppIdentity.BusinessLogicLayer
{
    public interface ICustomerLogic
    {
      Task<bool> InsertCustomer(Customer customer);
      Customer GetCustomerById(int id);
      int CreateCustomer(Customer customer);
      bool UpdateCustomer(Customer customer);
      bool DeleteCustomer(Customer customer);
    }
}

