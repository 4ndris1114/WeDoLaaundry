using WebAppIdentity.Models;
using WebAppIdentity.ServiceLayer;

namespace WebAppIdentity.BusinessLogicLayer
{
    public class BookingLogic : IBookingLogic
    {
        private readonly BookingService _bookingServiceAccess;

        public BookingLogic()
        {
            _bookingServiceAccess = new();
        }

        public async Task<bool> InsertBooking(Booking booking)
        {
            bool wasInserted;
            try
            {
                wasInserted = await _bookingServiceAccess.PostBooking(booking);
            }
            catch
            {
                wasInserted = false;
            }
            return wasInserted;
        }
    }
}
