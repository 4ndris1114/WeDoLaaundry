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

        Customer GetCustomerById(int id);

        List<Customer> getAllCustomers();

        int CreateCustomer(Customer customer);

    }
}
