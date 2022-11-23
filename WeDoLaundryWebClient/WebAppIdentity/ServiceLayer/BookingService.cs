using Newtonsoft.Json;
using System.Text;
using WebAppIdentity.Models;

namespace WebAppIdentity.ServiceLayer
{
    public class BookingService
    {
        static readonly string restUrl = "https://localhost:7091/api/bookings";
        readonly HttpClient _client;
        readonly HttpClientHandler _handler = new();

        public BookingService()
        {
            _client = new HttpClient();
            _handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
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
    }
}
