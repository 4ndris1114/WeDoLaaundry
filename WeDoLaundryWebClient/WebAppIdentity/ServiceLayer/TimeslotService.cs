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

        public async Task<int> GetByDayAndSlot(string date, string slot)
        {
            IntDeserial returnTimeSlot;
            int returnId = 0;

            var uri = new Uri(string.Format(restUrl + date + "/" + slot));

            try
            {
                var response = await _client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnTimeSlot = JsonConvert.DeserializeObject<IntDeserial>(content);
                    returnId = returnTimeSlot.Id;
                } else
                {
                    returnTimeSlot = new();
                }
            }
            catch
            {
                returnId = 0;
            }
            return returnId;
        }

        public async Task<TimeSlot> GetById(int id)
        {
            TimeSlot returnSlot;

            var uri = new Uri(string.Format(restUrl + id));

            try
            {
                var response = await _client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    returnSlot = JsonConvert.DeserializeObject<TimeSlot>(content);
                } else
                {
                    returnSlot = new();
                }
            }
            catch 
            {
                returnSlot = null;
            }
            return returnSlot;
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
