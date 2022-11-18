using Newtonsoft.Json;
using System.Text;
using WebAppProject.Models;

namespace WebAppProject.ServiceLayer
{
    public class CustomerService
    {

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

    }
}
