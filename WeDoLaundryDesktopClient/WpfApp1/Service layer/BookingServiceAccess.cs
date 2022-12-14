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
    public class BookingServiceAccess
    {
        static readonly string restUrl = "https://localhost:7091/api/bookings";
        readonly HttpClient _httpClient;
        public HttpStatusCode CurrentHttpStatusCode { get; set; }
        //static readonly string authenType = "Bearer";

        public BookingServiceAccess()
        {
            _httpClient = new();
        }
        public async Task<List<Booking>?> GetAll()
        {
            List<Booking>? returnList;

            var uri = new Uri(string.Format(restUrl));

            try
            {
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnList = JsonConvert.DeserializeObject<List<Booking>>(content);
                }
                else
                {
                    returnList = new();
                }
            }
            catch
            {
                returnList = new();
            }
            return returnList;
        }

        public async Task<bool> UpdateBookingAsync(int id, Booking booking)
        {
            bool wasUpdated;

            string useRestUrl = restUrl;
            bool hasValidId = (id > 1000);
            if (hasValidId)
            {
                useRestUrl = useRestUrl + "/" + id;
            }
            var uri = new Uri(string.Format(useRestUrl));

            try
            {
                var json = JsonConvert.SerializeObject(booking);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(uri, content);
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

        public async Task<bool> DeleteBookingAsync(int id, int pickupId, int deliveryId)
        {
            bool wasDeleted;
            var uri = new Uri(string.Format(restUrl + "/" + id));
            string restUrl2 = "https://localhost:7091/api/Timeslots/modify/";
            var uriPickUp = new Uri(string.Format(restUrl2 + pickupId + "/" + true + "/1"));
            var uriDelivery = new Uri(string.Format(restUrl2 + deliveryId + "/" + true + "/1"));

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    wasDeleted = true;
                    await _httpClient.PutAsync(uriPickUp, null);
                    await _httpClient.PutAsync(uriDelivery, null);
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
