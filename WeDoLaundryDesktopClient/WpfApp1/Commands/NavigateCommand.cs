using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Services;
using WpfApp1.Stores;
using WpfApp1.ViewModels;

namespace WpfApp1.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _navService;

        public NavigateCommand(NavigationService<TViewModel> navService)
        {
            _navService = navService;
        }

        public override void Execute(object parameter)
        {
            _navService.Navigate();
        }
    }
}
