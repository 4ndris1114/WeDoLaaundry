using Newtonsoft.Json;
using System.Text;
using WebAppIdentity.Models;

namespace WebAppIdentity.ServiceLayer
{
    public class BookingService : IBookingService
    {
        static readonly string restUrl = "https://localhost:7091/api/bookings/";
        readonly HttpClient _client;
        readonly HttpClientHandler _handler = new();

        public BookingService()
        {
            _client = new HttpClient();
            _handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        }

        public async Task<List<Booking>> GetCustomersBookings(int customerId)
        {
            List<Booking> returnList;

            var uri = new Uri(string.Format(restUrl + "customerBookings/" + customerId));

            try
            {
                var response = await _client.GetAsync(uri);
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
                returnList = null;
            }
            return returnList;
        }

        public async Task<bool> PostBooking(Booking booking)
        {
            bool wasPosted;
            var uri = new Uri(string.Format(restUrl));
            try
            {
                var json = JsonConvert.SerializeObject(booking);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, content);

                if(response.IsSuccessStatusCode)
                {
                    wasPosted = true;
                } else
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

        public async Task<Booking?> Get(int id)
        {
            Booking? returnBooking = null;

            var uri = new Uri(string.Format(restUrl + id));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnBooking = JsonConvert.DeserializeObject<Booking>(content);
                }
                else
                {
                    returnBooking = null;
                }
            }
            catch
            {
                returnBooking = null;
            }
            return returnBooking;
        }

        public async Task<List<Booking>?> GetAll()
        {
            List<Booking>? returnList = null;

            var uri = new Uri(string.Format(restUrl));

            try
            {
                var response = await _client.GetAsync(uri);
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

        public async Task<bool> DeleteBooking(int id, int pickupId, int deliveryId)
        {
            bool wasDeleted;
            var uri = new Uri(string.Format(restUrl+id));
            string restUrl2 = "https://localhost:7091/api/Timeslots/modify/";
            var uriPickUp = new Uri(string.Format(restUrl2 + pickupId + "/" + true));
            var uriDelivery = new Uri(string.Format(restUrl2 + deliveryId + "/" + true));

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    wasDeleted = true;
                    _client.PutAsync(uriPickUp, null);
                    _client.PutAsync(uriDelivery, null);
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
