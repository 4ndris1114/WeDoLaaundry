using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Controller_layer;
using WpfApp1.Model;
using WpfApp1.Model_layer;

namespace WpfApp1.ViewModels
{
    public class TimeslotListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<TimeslotViewModel> _timeslots;
        private readonly TimeslotsController _controller;

        public IEnumerable<TimeslotViewModel> Timeslots => _timeslots;

        public TimeslotListViewModel()
        {
            _controller = new();
            _timeslots = new ObservableCollection<TimeslotViewModel>();
            PopulateList();
        }

        private async void PopulateList()
        {
            List<TimeSlot> timeslotList = await _controller.GetTimeslotsAsync();

            if (timeslotList != null)
            {
                foreach (var timeslot in timeslotList)
                {
                    _timeslots.Add(new TimeslotViewModel(timeslot));
                }
            }
        }
    }
}
