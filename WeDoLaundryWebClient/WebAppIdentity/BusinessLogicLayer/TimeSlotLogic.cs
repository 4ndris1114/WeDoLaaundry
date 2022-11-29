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

        public async Task<TimeSlot> GetByDayAndSlot(DateTime date, string slot)
        {
            TimeSlot returnSlot;
            try
            {
                returnSlot = await _timeslotService.GetByDayAndSlot(date, slot);
            }
            catch 
            {
                returnSlot = null;
            }
            return returnSlot;
        }
    }
}
