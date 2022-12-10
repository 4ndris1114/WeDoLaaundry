using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Services;
using WpfApp1.Stores;

namespace WpfApp1.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public string WelcomeMessage => "Welcome to WeDoLaundry's management system - Home";

        public ICommand NavigateCustomersCommand { get; }

        public HomeViewModel(NavigationStore navStore)
        {
            NavigateCustomersCommand = new NavigateCommand<CustomerViewModel>(new NavigationService<CustomerViewModel>(
                navStore, () => new CustomerViewModel(navStore)));
        }
    }
}
