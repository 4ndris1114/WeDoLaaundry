using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.ServiceAccess;

namespace WpfApp1.Controller_layer
{
    public class CustomersController
    {
        CustomerServiceAccess _serviceAccess;

        public CustomersController()
        {
            _serviceAccess = new CustomerServiceAccess();
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            List<Customer> foundCustomers = null;

            foundCustomers = await _serviceAccess.GetCustomersAsync();


            return foundCustomers;

        }
    }
}
