using Newtonsoft.Json;
using WebAppIdentity.Models;

namespace WebAppIdentity.ServiceLayer
{
    public class TimeslotService
    {

        static readonly string restUrl = "https://localhost:7091/api/timeslots/";
        readonly HttpClient _client;
        readonly HttpClientHandler _clientHandler = new();

        public TimeslotService()
        {
            _client = new HttpClient();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        }

        public async Task<TimeSlot> Get(int id)
        {
            TimeSlot returnTimeslot;

            var uri = new Uri(string.Format(restUrl + id));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnTimeslot = JsonConvert.DeserializeObject<TimeSlot>(content);
                }
                else
                {
                    returnTimeslot = new();
                }
            }
            catch
            {
                returnTimeslot = null;
            }
            return returnTimeslot;
        }

        public async Task<TimeSlot> GetByDayAndSlot(DateTime date, string slot)
        {
            TimeSlot returnTimeSlot;

            var uri = new Uri(string.Format(restUrl + date + "/" + slot));

            try
            {
                var response = await _client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnTimeSlot = JsonConvert.DeserializeObject<TimeSlot>(content);
                } else
                {
                    returnTimeSlot = new();
                }
            }
            catch
            {
                returnTimeSlot = null;
            }
            return returnTimeSlot;
        }

        public async Task<List<TimeSlot>> GetAll()
        {
            List<TimeSlot> returnList;

            var uri = new Uri(string.Format(restUrl));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnList = JsonConvert.DeserializeObject<List<TimeSlot>>(content);
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

        public async Task<bool> Decrease(int id)
        {
            bool wasDecreased;

            var uri = new Uri(string.Format(restUrl+"decrease/"+id));

            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    wasDecreased = JsonConvert.DeserializeObject<bool>(content);
                }
                else
                {
                    wasDecreased = false;
                }
            }
            catch
            {
                wasDecreased = false;
            }
            return wasDecreased;
        }
    }
}
