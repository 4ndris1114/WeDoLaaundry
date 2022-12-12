using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Model_layer;

namespace WpfApp1.Service_layer
{
    public class DriverServiceAccess
    {
        static readonly string restUrl = "https://localhost:7091/api/drivers";
        readonly HttpClient _httpClient;
        public HttpStatusCode CurrentHttpStatusCode { get; set; }

        public  DriverServiceAccess()
        {
            _httpClient = new();
        }

        public async Task<List<Driver>?> GetAll()
        {
            List<Driver>? returnList = null;

            var uri = new Uri(string.Format(restUrl));

            try
            {
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnList = JsonConvert.DeserializeObject<List<Driver>>(content);
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

    }
}

