using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfApp1.Controller_layer;
using WpfApp1.Model;
using WpfApp1.Model_layer;

namespace WpfApp1.ViewModels
{
    public class BookingListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<BookingViewModel> _bookings;
        private readonly BookingsController _controller;
        private readonly TimeslotsController _timeSlotController;

        public IEnumerable<BookingViewModel> Bookings => _bookings;

        public BookingListViewModel()
        {
            _controller = new();
            _timeSlotController = new();
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
                    TimeSlot collectionTime = await _timeSlotController.GetById(booking.PickUpTimeId);
                    TimeSlot deliveryTime = await _timeSlotController.GetById(booking.ReturnTimeId);
                    string pickUpSlot = collectionTime.Date.ToString("dd/MM-yyyy") + "  " + collectionTime.Slot;
                    string returnSlot = deliveryTime.Date.ToString("dd/MM-yyyy") + " " + deliveryTime.Slot;
                    _bookings.Add(new BookingViewModel(booking, pickUpSlot, returnSlot));
                }
            }
        }
    }
}
