using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfApp1.Controller_layer;

namespace WpfApp1.ViewModels
{
    public class BookingListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<BookingViewModel> _bookings;
        private readonly BookingsController _controller;

        public IEnumerable<BookingViewModel> Bookings => _bookings;

        public BookingListViewModel()
        {
            _bookings = new ObservableCollection<BookingViewModel>();
        }
    }
}
