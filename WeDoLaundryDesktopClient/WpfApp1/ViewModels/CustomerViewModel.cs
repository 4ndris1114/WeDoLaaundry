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
    public class CustomerViewModel : ViewModelBase
    {
        public string WelcomeMessage => "Welcome to WeDoLaundry's management system - Customers";

        public ICommand NavigateHomeCommand { get; }

        public CustomerViewModel(NavigationStore navStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(new NavigationService<HomeViewModel>(
                navStore, () => new HomeViewModel(navStore)));
        }
    }
}
