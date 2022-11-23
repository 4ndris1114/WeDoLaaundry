using Data.Model_layer;
using Model.Model_layer;

namespace Service.BusinessLogicLayer
{
    public interface IBookingdataControl
    {
        List<Booking>? Get();
        Booking GetById(int id);
        int Add(Booking booking);
        bool Update(Booking customer);
        bool Delete(int id);
    }
}
