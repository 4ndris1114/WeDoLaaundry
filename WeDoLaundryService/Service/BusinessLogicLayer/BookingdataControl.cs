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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
