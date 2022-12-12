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
    }
}
