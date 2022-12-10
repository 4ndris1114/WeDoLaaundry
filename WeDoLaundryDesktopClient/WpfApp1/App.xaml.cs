using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using WpfApp1.Services;
using WpfApp1.Stores;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navStore;


        public App()
        {
            _navStore = new();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService<HomeViewModel> homeNavigationService = CreateHomeNavigationService();
            homeNavigationService.Navigate();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private INavigationService<HomeViewModel> CreateHomeNavigationService()
        {
            return new LayoutNavigationService<HomeViewModel>(
                _navStore, 
                () => new HomeViewModel(CreateCustomerNavigationService()),
                CreateNavigationBarViewModel);
        }

        private INavigationService<CustomerViewModel> CreateCustomerNavigationService()
        {
            return new LayoutNavigationService<CustomerViewModel>(
                _navStore, 
                () => new CustomerViewModel(CreateHomeNavigationService()),
                CreateNavigationBarViewModel);
        }

        private NavigationBarViewModel CreateNavigationBarViewModel()
        {
            return new NavigationBarViewModel(
                CreateHomeNavigationService(),
                CreateCustomerNavigationService());
        }
    }
}
