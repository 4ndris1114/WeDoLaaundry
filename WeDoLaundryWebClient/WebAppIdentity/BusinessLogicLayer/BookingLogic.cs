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
        public async Task<List<Booking>?> GetAll()
        {
            List<Booking>? returnList = null;
            try
            {
                returnList = await _bookingServiceAccess.GetAll();
            }
            catch { 
                returnList = null;
            }
            return returnList;
        }

        public async Task<List<Booking>> GetCustomersBookings(int customerId)
        {
            List<Booking> returnList;
            try
            {
                returnList = await _bookingServiceAccess.GetCustomersBookings(customerId);
            }
            catch 
            {
                returnList = null;
            }
            return returnList;
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
