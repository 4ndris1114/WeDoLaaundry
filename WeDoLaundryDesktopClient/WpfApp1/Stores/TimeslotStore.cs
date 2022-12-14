using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model_layer;

namespace WpfApp1.Stores
{
    public class TimeslotStore
    {
        public event Action<TimeSlot> TimeslotAdded;

        public void AddTimeslot(TimeSlot timeslot)
        {
            TimeslotAdded?.Invoke(timeslot);
        }
    }
}
