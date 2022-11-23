using DataAccess.Database_layer;
using Model.Model_layer;

namespace Service.BusinessLogicLayer
{
    public class BookingdataControl : IBookingdataControl
    {
        private readonly IBookingDatabaseAccess _bookingDbAccess;

        public BookingdataControl(IConfiguration configuration)
        {
            _bookingDbAccess = new BookingDatabaseAccess(configuration);
        }

        public int Add(Booking booking)
        {
            int insertedId;
            try
            {
                insertedId = _bookingDbAccess.CreateBooking(booking);
            }
            catch
            {
                insertedId = -1;
            }
            return insertedId;
        }

        public List<Booking>? Get()
        {
            List<Booking>? foundBookings = null;
            try
            {
                foundBookings = _bookingDbAccess.GetAll();
            }
            catch
            {
                foundBookings = null;
            }
            return foundBookings;
        }

        public Booking GetById(int id)
        {
            Booking? foundBooking;
            try
            {
                foundBooking = _bookingDbAccess.Get(id);
            }
            catch
            {
                foundBooking = null;
            }
            return foundBooking;
        }
    }
}
