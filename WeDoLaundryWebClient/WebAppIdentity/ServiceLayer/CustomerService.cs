using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using WebAppIdentity.BusinessLogicLayer;
using WebAppIdentity.Models;

namespace WebAppIdentity.ServiceLayer
{
    public class CustomerService : ICustomerService
    {

        static readonly string restUrl = "https://localhost:7091/api/customers/";
        readonly HttpClient _client;
        readonly HttpClientHandler _clientHandler = new();

        public CustomerService()
        {
            _client = new HttpClient();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        }

        public async Task<Customer> GetCustomerByUserId(string id)
        {
            Customer returnCustomer;

            var uri = new Uri(string.Format(restUrl + "account/" + id));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NoContent)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnCustomer = JsonConvert.DeserializeObject<Customer>(content);
                }
                else //if 204 or 500
                {
                    returnCustomer = null;
                }
            }
            catch
            {
                returnCustomer = null;
            }
            return returnCustomer;
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

                if (response.IsSuccessStatusCode)
                {
                    wasPosted = true;
                }
                else
                {
                    wasPosted = false;
                }
            }
            catch
            {
                wasPosted = false;
            }

            return wasPosted;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            bool wasUpdated;

            var uri = new Uri(string.Format(restUrl + customer.Id));

            try
            {
                var json = JsonConvert.SerializeObject(customer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync(uri, content);
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

        public async Task<bool> UpdateSubscription(Customer customer)
        {
            bool wasUpdated;

            var uri = new Uri(string.Format(restUrl + customer.Id + "/" + customer.CustomerType));

            try
            {
                var json = JsonConvert.SerializeObject(customer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    wasUpdated = true;
                }
                else
                {
                    wasUpdated = new();
                }
            }
            catch
            {
                wasUpdated = false;
            }
            return wasUpdated;
        }

        public async Task<bool> DeleteCustomer(Customer customer)
        {
            bool wasDeleted;

            var uri = new Uri(string.Format(restUrl + "userId/" + customer));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    wasDeleted = JsonConvert.DeserializeObject<bool>(content);
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


