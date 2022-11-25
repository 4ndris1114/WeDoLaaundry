﻿using Data.Model_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model_layer
{
    public class TimeSlot
    {

        public DateTime Date { get; set; }
        public String Slot { get; set; }
        public int Availability { get; set; }


        public TimeSlot(DateTime date, String slot, int availability)
        {
            Date = date;
            Slot = slot;
            Availability = availability;
        }

        public TimeSlot()
        {

        }
    }
}
