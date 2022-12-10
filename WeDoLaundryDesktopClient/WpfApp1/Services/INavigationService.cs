using WpfApp1.ViewModels;

namespace WpfApp1.Services
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        void Navigate();
    }
}