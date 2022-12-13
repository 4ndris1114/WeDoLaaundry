using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfApp1.Controller_layer;
using WpfApp1.Model;

namespace WpfApp1.ViewModels
{
    public class BookingListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<BookingViewModel> _bookings;
        private readonly BookingsController _controller;

        public IEnumerable<BookingViewModel> Bookings => _bookings;

        public BookingListViewModel()
        {
            _controller = new();
            _bookings = new ObservableCollection<BookingViewModel>();
            PopulateList();
        }

        private async void PopulateList()
        {
            List<Booking> bookingList = await _controller.GetBookingsAsync();

            if (bookingList != null)
            {
                foreach (var booking in bookingList)
                {
                    _bookings.Add(new BookingViewModel(booking));
                }
            }
        }
    }
}
