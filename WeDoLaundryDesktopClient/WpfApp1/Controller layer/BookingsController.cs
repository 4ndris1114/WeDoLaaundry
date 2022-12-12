using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.ServiceAccess;

namespace WpfApp1.Controller_layer
{
    public class BookingsController
    {
        BookingServiceAccess _serviceAccess;

        public BookingsController()
        {
            _serviceAccess = new BookingServiceAccess();
        }

        public Task<List<Booking>> GetBookingsAsync()
        {
            return GetBookingsAsync(_serviceAccess);
        }

        public async Task<List<Booking>> GetBookingsAsync(BookingServiceAccess _serviceAccess)
        {
            List<Booking> foundBookings = null;

            foundBookings = await _serviceAccess.GetAll();

            return foundBookings;

        }

        public async Task<List<Booking>> GetCustomersBookingsAsync(int customerId)
        {
            List<Booking> foundBookings = null;

            foundBookings = await _serviceAccess.GetCustomersBookingsAsync(customerId);

            return foundBookings;

        }
    }
}
