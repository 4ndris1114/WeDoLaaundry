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

        public CustomerServiceAccess()
        {
            _httpClient = new();
        }

        public async Task<List<Customer>> GetCustomersAsync(int id = -1)
        {
            List<Customer> customerList = null;

            // api/customer/{id}
            string useRestUrl = restUrl;
            bool hasValidId = (id > 0);
            if (hasValidId)
            {
                useRestUrl += id;
            }
            var uri = new Uri(string.Format(useRestUrl));

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
    }
}
