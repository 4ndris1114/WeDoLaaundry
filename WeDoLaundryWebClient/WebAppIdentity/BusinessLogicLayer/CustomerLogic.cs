using WebAppIdentity.Models;
using WebAppIdentity.ServiceLayer;

namespace WebAppIdentity.BusinessLogicLayer
{
    public class CustomerLogic //need to create interface
    {

        private readonly CustomerService _customerServiceAccess;

        public CustomerLogic()
        {
            _customerServiceAccess = new();
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
    }
}
