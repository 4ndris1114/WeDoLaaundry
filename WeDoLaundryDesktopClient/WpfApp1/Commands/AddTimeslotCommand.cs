using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Controller_layer;
using WpfApp1.Model_layer;
using WpfApp1.Services;
using WpfApp1.Stores;
using WpfApp1.ViewModels;

namespace WpfApp1.Commands
{
    public class AddTimeslotCommand : CommandBase
    {
        private readonly AddTimeslotViewModel _addTimeslotViewModel;
        private readonly TimeslotStore _timeslotStore;
        private readonly INavigationService _navigationService;

        private readonly TimeslotsController _controller;

        public AddTimeslotCommand(AddTimeslotViewModel addTimeslotViewModel, TimeslotStore timeslotStore, INavigationService navigationService)
        {
            _addTimeslotViewModel = addTimeslotViewModel;
            _timeslotStore = timeslotStore;
            _navigationService = navigationService;

            _controller = new();
        }

        public override async void Execute(object paramter)
        {
            DateTime date = _addTimeslotViewModel.Date;
            string slot = _addTimeslotViewModel.Slot;
            int availability = _addTimeslotViewModel.Availability;

            TimeSlot toBeAdded = new(-1, date,slot,availability);
            if (/*date > DateTime.Now &&*/ availability > 0)
            {
                toBeAdded.Id = await _controller.Add(toBeAdded);
                if (toBeAdded.Id > 0)
                {
                    _timeslotStore.AddTimeslot(toBeAdded);
                }
                _navigationService.Navigate();
            } else
            {
                MessageBox.Show("Couldn't be added!");
            }
        }
    }
}
