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

        //public async Task<List<Booking>> GetCustomersBookingsAsync(int customerId)
        //{
        //    List<Booking> returnList;

        //    var uri = new Uri(string.Format(restUrl + "customerBookings/" + customerId));

        //    try
        //    {
        //        var response = await _httpClient.GetAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            returnList = JsonConvert.DeserializeObject<List<Booking>>(content);
        //        }
        //        else
        //        {
        //            returnList = new();
        //        }
        //    }
        //    catch
        //    {
        //        returnList = null;
        //    }
        //    return returnList;
        //}

    }
}
