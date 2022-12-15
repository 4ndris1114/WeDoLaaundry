using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Controller_layer;
using WpfApp1.Model;
using WpfApp1.Services;
using WpfApp1.Stores;

namespace WpfApp1.ViewModels
{
    public class DriverListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<DriverViewModel> _drivers;
        private readonly DriversController _controller;

        public IEnumerable<DriverViewModel> Drivers => _drivers;

        public DriverListViewModel()
        {
            _controller = new();
            _drivers = new ObservableCollection<DriverViewModel>();
            PopulateList();
        }

        private async void PopulateList()
        {
            List<Driver> driverList = await _controller.GetDriversAsync();

            if (driverList != null)
            {
                foreach (var driver in driverList)
                {
                    _drivers.Add(new DriverViewModel(driver));
                }
            }
        }
    }
}
