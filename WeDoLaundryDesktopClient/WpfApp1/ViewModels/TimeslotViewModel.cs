using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model_layer;

namespace WpfApp1.ViewModels
{
    public class TimeslotViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }
        public string Date { get; set; }
        public string Slot { get; set; }
        private int _availability;

        public int Availability
        {
            get { return _availability; }
            set 
            { 
                _availability = value;
                NotifyPropertyChanged("Availability");
            }
        }

        public TimeslotViewModel(TimeSlot timeslot)
        {
            Id = timeslot.Id;
            Date = timeslot.Date.ToString("yyyy MMMM dd");
            Slot = timeslot.Slot;
            Availability = timeslot.Availability;
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
