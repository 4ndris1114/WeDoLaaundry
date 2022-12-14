using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Model_layer;
using WpfApp1.Service_layer;

namespace WpfApp1.Controller_layer
{
    public class TimeslotsController
    {
        TimeslotServiceAccess _serviceAccess;

        public TimeslotsController()
        {
            _serviceAccess = new();
        }

        public async Task<List<TimeSlot>> GetTimeslotsAsync()
        {
            List<TimeSlot> foundTimeslots = null;

            foundTimeslots = await _serviceAccess.GetTimeslotsAsync();

            return foundTimeslots;
        }

        public async Task<List<string>> GetTimeslotAddressesAsync(int id)
        {
            List<string> foundAddresses = null;

            foundAddresses = await _serviceAccess.GetTimeslotAddressesAsync(id);

            return foundAddresses;
        }

        public async Task<TimeSlot> GetById(int id)
        {
            TimeSlot returnSlot;
            try
            {
                returnSlot = await _serviceAccess.GetById(id);
            }
            catch
            {
                returnSlot = null;
            }
            return returnSlot;
        }

        public async Task<bool> Modify(int id, bool mode, int value)
        {
            bool wasModified = false;

            wasModified = await _serviceAccess.Modify(id, mode, value);

            return wasModified;
        }

        public async Task<int> Add(TimeSlot timeslot)
        {
            int insertedId = -1;

            if (timeslot != null)
            {
                try
                {
                    insertedId = await _serviceAccess.Post(timeslot);
                }
                catch (Exception)
                {
                    insertedId = -1;
                }
            }

            return insertedId;
        }

        public async Task<bool> Delete(int id)
        {
            bool wasDeleted = false;

            wasDeleted = await _serviceAccess.Delete(id);

            return wasDeleted;
        }
    }
}
