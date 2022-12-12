using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfApp1.Controller_layer;

namespace WpfApp1.ViewModels
{
    public class DriverListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<DriverViewModel> _drivers;
        private readonly DriversController _controller;

        public IEnumerable<DriverViewModel> Drivers => _drivers;

        public DriverListViewModel()
        {
            _drivers = new ObservableCollection<DriverViewModel>();
        }
    }
}
