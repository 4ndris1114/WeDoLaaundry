using WebAppIdentity.Models;
using WebAppIdentity.ServiceLayer;

namespace WebAppIdentity.BusinessLogicLayer
{
    public class TimeSlotLogic : ITimeSlotLogic
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

        public async Task<bool> validateOrder(int id1, int id2)
        {
            TimeSlot n1 = await GetById(id1);
            DateTime d1 = n1.Date;
            int i1 = Convert.ToInt16(n1.Slot.Split('-')[1]); // End of slot 1

            TimeSlot n2 = await GetById(id2);
            DateTime d2 = n2.Date;
            int i2 = Convert.ToInt16(n2.Slot.Split('-')[0]); // Start of slot 2
            if (d1.CompareTo(d2) < 0) {
                return true;
            }
            if (d1.CompareTo(d2) == 0 && i1 <= i2) {
                return true;
            }
            return false;
        }
    }
}
