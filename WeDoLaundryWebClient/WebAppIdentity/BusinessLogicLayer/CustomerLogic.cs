using WebAppIdentity.Models;
using WebAppIdentity.Data.Migrations;
using WebAppIdentity.ServiceLayer;

namespace WebAppIdentity.BusinessLogicLayer
{
    public class CustomerLogic : ICustomerLogic
    {

        private readonly ICustomerService _customerServiceAccess;

        public CustomerLogic()
        {
            _customerServiceAccess = new CustomerService();
        }

        public async Task<bool> InsertCustomer(Customer customer)
        {
            bool wasInserted;
            try
            {
                wasInserted = await _customerServiceAccess.PostCustomer(customer);
            }
            catch
            {
                wasInserted = false;
            }

            return wasInserted;
        }
        public Customer? GetCustomerById(int id)
        {
            Customer? foundCustomer;
            try
            {
                foundCustomer = _customerServiceAccess.GetCustomerById(id);
            }
            catch
            {
                foundCustomer = null;
            }
            return foundCustomer;
        }

        public int CreateCustomer(Customer customer)
        {
            int wasCreated;
            try
            {
                wasCreated = _customerServiceAccess.CreateCustomer(customer);
            }
            catch
            {
                wasCreated = -1;
            }
            return wasCreated;
        }


        public bool UpdateCustomer(Customer customer)
        {
            bool wasUpdated;
            try
            {
                wasUpdated = _customerServiceAccess.UpdateCustomer(customer);
            }
            catch
            {
                wasUpdated= false;
            }
            return wasUpdated;
        }

        public bool DeleteCustomer(Customer customer)
        {
            bool wasDeleted;
            try
            {
                wasDeleted = _customerServiceAccess.DeleteCustomer(customer);
            }
            catch
            {
                wasDeleted = false;
            }
            return wasDeleted;
        }

    }
}

