using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp1.Commands;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateCustomersCommand { get; }
        public ICommand NavigateBookingsCommand { get; }
        public ICommand NavigateDriversCommand { get; }
        public ICommand NavigateTimeslotsCommand { get; }

        public NavigationBarViewModel(INavigationService homeNavigationService, 
            INavigationService customerNavigationService, 
            INavigationService bookingNavigationService,
            INavigationService driverNavigationService ,
            INavigationService timeslotNavigationService)
        {
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            NavigateCustomersCommand = new NavigateCommand(customerNavigationService);
            NavigateBookingsCommand = new NavigateCommand(bookingNavigationService);
            NavigateDriversCommand = new NavigateCommand(driverNavigationService);
            NavigateTimeslotsCommand = new NavigateCommand(timeslotNavigationService);
        }
    }
}
