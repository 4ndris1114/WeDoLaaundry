using Data.Model_layer;
using Model.Model_layer;

namespace Service.BusinessLogicLayer
{
    public interface IBookingdataControl
    {
        List<Booking>? Get();
        Booking GetById(int id);
        int Add(Booking booking);

        List<Booking> GetCustomersBookings(int customerId);
        //bool Update(Booking customer);
        bool Delete(int id);
        List<string> GetAddressesByTimeslotId(int id);
        bool Update(Booking Booking);
    }
}
