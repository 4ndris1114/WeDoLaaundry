using WebAppIdentity.Models;

namespace WebAppIdentity.ServiceLayer
{
    public interface IBookingService
    {
        Task<List<Booking>> GetCustomersBookings(int customerId);
        Task<bool> PostBooking(Booking booking);
        Task<List<Booking>?> GetAll();
    }
}
