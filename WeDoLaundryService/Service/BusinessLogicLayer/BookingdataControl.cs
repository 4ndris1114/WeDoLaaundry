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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Booking>? Get()
        {
            throw new NotImplementedException();
        }

        public Booking GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Booking customer)
        {
            throw new NotImplementedException();
        }
    }
}
