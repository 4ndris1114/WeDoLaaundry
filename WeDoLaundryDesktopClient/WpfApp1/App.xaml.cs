using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            //Dependency Injection
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<NavigationStore>();

            services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));

            services.AddTransient<HomeViewModel>(s => new HomeViewModel(CreateCustomerNavigationService(s)));
            services.AddTransient<CustomerViewModel>(s => new CustomerViewModel(CreateHomeNavigationService(s)));
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<MainWindow>(s => new WpfApp1.MainWindow
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
                () => CreateNavigationBarViewModel(serviceProvider));
        }

        private INavigationService CreateCustomerNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<CustomerViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(), 
                () => serviceProvider.GetRequiredService<CustomerViewModel>(),
                () => CreateNavigationBarViewModel(serviceProvider));
        }

        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(
                CreateHomeNavigationService(serviceProvider),
                CreateCustomerNavigationService(serviceProvider));
        }
    }
}
