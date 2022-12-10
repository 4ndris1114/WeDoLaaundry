using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Stores;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navStore;

        public ViewModelBase CurrentViewModel => _navStore.CurrentViewModel;

        public MainViewModel(NavigationStore navStore)
        {
            _navStore = navStore;

            _navStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void  OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
