using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpfApp1.Model;
using WpfApp1.Model_layer;

namespace WpfApp1.Service_layer
{
    public class TimeslotServiceAccess
    {
        static readonly string restUrl = "https://localhost:7091/api/Timeslots";
        readonly HttpClient _httpClient;
        public HttpStatusCode CurrentHttpStatusCode { get; set; }

        public TimeslotServiceAccess()
        {
            _httpClient = new();
        }

        public async Task<List<TimeSlot>> GetTimeslotsAsync(int id = -1)
        {
            List<TimeSlot> timeslotList = null;

            // /api/timeslots/{id}
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
                        TimeSlot foundTimeslot = JsonConvert.DeserializeObject<TimeSlot>(content);
                        if (foundTimeslot != null)
                        {
                            timeslotList = new List<TimeSlot>() { foundTimeslot };
                        }
                    }
                    else
                    {
                        timeslotList = JsonConvert.DeserializeObject<List<TimeSlot>>(content);
                    }
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        timeslotList = new List<TimeSlot>();
                    }
                    else
                    {
                        timeslotList = null;
                    }
                }
            }
            catch
            {
                timeslotList = null;
            }
            return timeslotList;
        }

    }
}
