using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels
{
    public class BookingListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<BookingViewModel> _bookings;

        public IEnumerable<BookingViewModel> Bookings => _bookings;

        public BookingListViewModel()
        {
            _bookings = new ObservableCollection<BookingViewModel>();
        }
    }
}
