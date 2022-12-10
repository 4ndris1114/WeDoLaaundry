using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Stores;
using WpfApp1.ViewModels;

namespace WpfApp1.Services
{
    public class NavigationService<TViewModel>
        where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navStore, Func<TViewModel> createViewModel)
        {
            _navStore = navStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navStore.CurrentViewModel = _createViewModel();
        }
    }
}
