using WebAppIdentity.Models;

namespace WebAppIdentity.ServiceLayer
{
    public interface IBookingService
    {
        Task<bool> PostBooking(Booking booking);
        Task<List<Booking>?> GetAll();
    }
}
