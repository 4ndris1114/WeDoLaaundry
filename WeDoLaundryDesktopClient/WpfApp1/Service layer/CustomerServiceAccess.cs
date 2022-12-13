using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.ServiceAccess
{
    public class CustomerServiceAccess
    {
        static readonly string restUrl = "https://localhost:7091/api/customers";
        readonly HttpClient _httpClient;
        public HttpStatusCode CurrentHttpStatusCode { get; set; }
        //static readonly string authenType = "Bearer";

        public CustomerServiceAccess()
        {
            _httpClient = new();
        }

        public async Task<List<Customer>> GetCustomersAsync(/*string tokenToUse, */int id = -1)
        {
            List<Customer> customerList = null;

            // /api/customers/{id}
            string useRestUrl = restUrl;
            bool hasValidId = (id > 0);
            if (hasValidId)
            {
                useRestUrl += id;
            }
            var uri = new Uri(string.Format(useRestUrl));

            //// Must add Bearer token to request header
            //string bearerTokenValue = authenType + " " + tokenToUse;
            //_httpClient.DefaultRequestHeaders.Remove("Authorization");          // To avoid more Authorization headers
            //_httpClient.DefaultRequestHeaders.Add("Authorization", bearerTokenValue);

            // Perform and evaluate the request
            try
            {
                var response = await _httpClient.GetAsync(uri);
                CurrentHttpStatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (hasValidId)
                    {
                        Customer foundCustomer = JsonConvert.DeserializeObject<Customer>(content);
                        if (foundCustomer != null)
                        {
                            customerList = new List<Customer>() { foundCustomer };
                        }
                    }
                    else
                    {
                        customerList = JsonConvert.DeserializeObject<List<Customer>>(content);
                    }
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        customerList = new List<Customer>();
                    }
                    else
                    {
                        customerList = null;
                    }
                }
            }
            catch
            {
                customerList = null;
            }
            return customerList;
        }

        public async Task<bool> UpdateCustomerAsync(int id, Customer customer)
        {
            bool wasUpdated;

            string useRestUrl = restUrl;
            bool hasValidId = (id > 0);
            if (hasValidId)
            {
                useRestUrl = useRestUrl + "/" + id;
            }
            var uri = new Uri(string.Format(useRestUrl));

            try
            {
                var response = await _httpClient.PutAsync(uri, content); // finish the shit
                if (response.IsSuccessStatusCode)
                {
                    wasUpdated = true;
                }
                else
                {
                    wasUpdated = false;
                }
            }
            catch
            {
                wasUpdated = false;
            }

            return wasUpdated;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            bool wasDeleted;

            string useRestUrl = restUrl;
            bool hasValidId = (id > 0);
            if (hasValidId)
            {
                useRestUrl = useRestUrl + "/" + id;
            }
            var uri = new Uri(string.Format(useRestUrl));

            try
            {
                var response = await _httpClient.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    wasDeleted = true;
                }
                else
                {
                    wasDeleted = false;
                }
            }
            catch
            {
                wasDeleted = false;
            }
            return wasDeleted;
        }
    }
}
