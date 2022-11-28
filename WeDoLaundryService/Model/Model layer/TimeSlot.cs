using Data.Model_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model_layer
{
    public class TimeSlot
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public String Slot { get; set; }
        public int Availability { get; set; }


        public TimeSlot(int id, DateTime date, String slot, int availability)
        {
            Id = id;
            Date = date;
            Slot = slot;
            Availability = availability;
        }

        public TimeSlot()
        {

        }
    }
}
