using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            //Dependency Injection
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<NavigationStore>();
            services.AddSingleton<TimeslotStore>();
            services.AddSingleton<ModalNavigationStore>();
            services.AddSingleton<CloseModalNavigationService>();

            services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));

            services.AddTransient<HomeViewModel>(s => new HomeViewModel(CreateCustomerNavigationService(s)));
            services.AddTransient<CustomerListViewModel>(s => new CustomerListViewModel());
            services.AddTransient<BookingListViewModel>(s => new BookingListViewModel());
            services.AddTransient<DriverListViewModel>(s => new DriverListViewModel());
            services.AddTransient<TimeslotListViewModel>(s => new TimeslotListViewModel(s.GetRequiredService<TimeslotStore>(), CreateAddTimeslotNavigationService(s)));
            services.AddTransient<AddTimeslotViewModel>(s => new AddTimeslotViewModel(s.GetRequiredService<TimeslotStore>(), s.GetRequiredService<CloseModalNavigationService>()));
            services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(), 
                () => serviceProvider.GetRequiredService<HomeViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }

        private INavigationService CreateCustomerNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<CustomerListViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(), 
                () => serviceProvider.GetRequiredService<CustomerListViewModel>()  ,
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }
        
        private INavigationService CreateBookingNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<BookingListViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<BookingListViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }

        private INavigationService CreateTimeslotNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<TimeslotListViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<TimeslotListViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }

        private INavigationService CreateDriverNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<DriverListViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<DriverListViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }

        private INavigationService CreateAddTimeslotNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<AddTimeslotViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                () => serviceProvider.GetRequiredService<AddTimeslotViewModel>());
        }

        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(
                CreateHomeNavigationService(serviceProvider),
                CreateCustomerNavigationService(serviceProvider),
                CreateBookingNavigationService(serviceProvider),
                CreateDriverNavigationService(serviceProvider),
                CreateTimeslotNavigationService(serviceProvider));
        }
    }
}
