using WebAppIdentity.Models;
using WebAppIdentity.ServiceLayer;

namespace WebAppIdentity.BusinessLogicLayer
{
    public class BookingLogic : IBookingLogic
    {
        private readonly BookingService _bookingService;

        public BookingLogic()
        {
            _bookingService = new();
        }

        public async Task<List<Booking>> GetAll()
        {
            List<Booking> returnList;
            try
            {
                returnList = await _bookingService.GetAll();
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
                wasInserted = await _bookingService.PostBooking(booking);
            }
            catch
            {
                wasInserted = false;
            }
            return wasInserted;
        }
    }
}
