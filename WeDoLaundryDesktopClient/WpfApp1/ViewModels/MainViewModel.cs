using Accessibility;
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
        private readonly ModalNavigationStore _modalNavigationStore;

        public ViewModelBase CurrentViewModel => _navStore.CurrentViewModel;
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public MainViewModel(NavigationStore navStore, ModalNavigationStore modalNavigationStore)
        {
            _navStore = navStore;
            _modalNavigationStore = modalNavigationStore;

            _navStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        }

        private void  OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}
