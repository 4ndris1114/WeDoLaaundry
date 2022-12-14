using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Controller_layer;
using WpfApp1.Model;
using WpfApp1.Model_layer;
using WpfApp1.Services;
using WpfApp1.Stores;

namespace WpfApp1.ViewModels
{
    public class TimeslotListViewModel : ViewModelBase
    {
        private readonly TimeslotStore _timeslotStore;

        private readonly ObservableCollection<TimeslotViewModel> _timeslots;
        private readonly ObservableCollection<TimeslotViewModel> _addresses;
        private readonly TimeslotsController _controller;

        public IEnumerable<TimeslotViewModel> Timeslots => _timeslots;
        public IEnumerable<TimeslotViewModel> Addresses => _addresses;

        public ICommand AddTimeslotCommand { get; }

        public TimeslotListViewModel(TimeslotStore timeslotStore, INavigationService addTimeslotNavigationService)
        {
            _timeslotStore = timeslotStore;
            AddTimeslotCommand = new NavigateCommand(addTimeslotNavigationService);

            _controller = new();
            _timeslots = new ObservableCollection<TimeslotViewModel>();
            PopulateList();

            _timeslotStore.TimeslotAdded += OnTimeslotAdded;
        }

        private void OnTimeslotAdded(TimeSlot timeslot)
        {
            _timeslots.Add(new TimeslotViewModel(timeslot));
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
