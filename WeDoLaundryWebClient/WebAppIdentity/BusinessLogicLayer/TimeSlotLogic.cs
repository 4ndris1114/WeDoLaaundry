using WebAppIdentity.Models;
using WebAppIdentity.ServiceLayer;

namespace WebAppIdentity.BusinessLogicLayer
{
    public class TimeSlotLogic
    {
        private readonly TimeslotService _timeslotService;

        public TimeSlotLogic()
        {
            _timeslotService = new();
        }

        public async Task<List<TimeSlot>> GetAll()
        {
            List<TimeSlot> returnList;
            try
            {
                returnList = await _timeslotService.GetAll();
            }
            catch 
            {
                returnList = null;
            }
            return returnList;
        }

        public async Task<TimeSlot> GetById(int id)
        {
            TimeSlot returnSlot;
            try
            {
                returnSlot = await _timeslotService.GetById(id);
            }
            catch
            {
                returnSlot = null;
            }
            return returnSlot;
        }

        public async Task<int> GetByDayAndSlot(string date, string slot)
        {
            int returnSlot;
            try
            {
                returnSlot = await _timeslotService.GetByDayAndSlot(date, slot);
            }
            catch 
            {
                returnSlot = 0;
            }
            return returnSlot;
        }
    }
}
