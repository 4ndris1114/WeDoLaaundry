using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model_layer;

namespace WpfApp1.ViewModels
{
    public class TimeslotViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Slot { get; set; }
        public int Availability { get; set; }

        public TimeslotViewModel(TimeSlot timeslot)
        {
            Id = timeslot.Id;
            Date = timeslot.Date.ToString("yyyy MMMM dd");
            Slot = timeslot.Slot;
            Availability = timeslot.Availability;
        }
    }
}
