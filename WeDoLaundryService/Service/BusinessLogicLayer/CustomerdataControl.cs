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

        public Customer? Get(int id)
        {
            Customer? foundCustomer = null;
            try
            {
                foundCustomer = _customerAccess.GetCustomerById(id);
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
    }
}
