using Data.Database_layer;
using Data.Model_layer;
using DataAccess;

namespace Service.BusinessLogicLayer
{
    public class CustomerdataControl : ICustomerdata
    {

        ICustomerAccess _customerAccess;

        public CustomerdataControl(IConfiguration config)
        {
            _customerAccess = new CustomerDatabaseAccess(config);
        }

        public CustomerdataControl()
        {
        }

        public List<Customer>? Get()
        {
            List<Customer>? foundCustomers = null;
            try
            {
                foundCustomers = _customerAccess.getAllCustomers();
            }
            catch
            {

                foundCustomers = null;
            }
            return foundCustomers;
        }

        public Customer? GetById(int id)
        {
            Customer? foundCustomer;
            try
            {
                foundCustomer = _customerAccess.GetById(id);
            }
            catch
            {

                foundCustomer = null;
            }
            return foundCustomer;
        }

        public Customer? GetByUserId(string userId)
        {
            Customer? foundCustomer;
            try
            {
                foundCustomer = _customerAccess.GetByUserId(userId);
            }
            catch
            {

                foundCustomer = null;
            }
            return foundCustomer;
        }

        public int Add(Customer customer)
        {
            int insertedId;
            try
            {
                insertedId = _customerAccess.CreateCustomer(customer);
            }
            catch
            {
                insertedId = -1;
            }
            return insertedId;
        }

        public bool Update(Customer customer)
        {
            bool wasUpdated;
            try
            {
                wasUpdated = _customerAccess.UpdateCustomer(customer);
            }
            catch
            {
                wasUpdated = false;
            }
            return wasUpdated;
        }

        public bool UpdateSub(int id, int subscription)
        {
            bool wasUpdated;
            try
            {
                wasUpdated = _customerAccess.UpdateSubscription(id, subscription);
            }
            catch
            {
                wasUpdated = false;
            }
            return wasUpdated;
        }

        public bool Delete(int id)
        {
            bool wasDeleted; 
            try
            {
                wasDeleted = _customerAccess.DeleteCustomer(id);
            }
            catch 
            {
                wasDeleted = false;
            }
            return wasDeleted;
        }
    }
}
