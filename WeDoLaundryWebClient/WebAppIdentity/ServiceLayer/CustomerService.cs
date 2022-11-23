using Newtonsoft.Json;
using System.Text;
using WebAppIdentity.BusinessLogicLayer;
using WebAppIdentity.Models;

namespace WebAppIdentity.ServiceLayer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerLogic _customerLogic;
        static readonly string restUrl = "https://localhost:7091/api/customers";
        readonly HttpClient _client;
        readonly HttpClientHandler _clientHandler = new();

        public CustomerService()
        {
            _client = new HttpClient();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<bool> PostCustomer(Customer customer)
        {
            bool wasPosted = false;

            var uri = new Uri(string.Format(restUrl));

            try
            {
                var json = JsonConvert.SerializeObject(customer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode) {
                    wasPosted = true;
                } else
                {
                    wasPosted = false;
                }
            }
            catch {
                wasPosted = false;
            }

            return wasPosted;
        }
            public Customer? GetCustomerById(int id)
            {
            Customer? foundCustomer;
            try
            {
                foundCustomer = _customerLogic.GetCustomerById(id);
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
                wasCreated = _customerLogic.CreateCustomer(customer);
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
                wasUpdated = _customerLogic.UpdateCustomer(customer);
            }
            catch
            {
                wasUpdated = false;
            }
            return wasUpdated;
        }

        public bool DeleteCustomer(Customer customer)
        {
            bool wasDeleted;
            try
            {
                wasDeleted = _customerLogic.DeleteCustomer(customer);
            }
            catch
            {
                wasDeleted = false;
            }
            return wasDeleted;
        }
    }

}

