using Data.Model_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database_layer
{
    public interface ICustomerAccess
    {

        Customer GetById(int id);

        Customer GetByUserId(string userId);

        List<Customer> getAllCustomers();

        int CreateCustomer(Customer customer);

        bool UpdateCustomer(Customer customer);

        bool UpdateSubscription(int id, int subscription);

        bool DeleteCustomer(int id);

    }
}
