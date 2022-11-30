using WebAppIdentity.Models;

namespace WebAppIdentity.BusinessLogicLayer
{
    public interface IBookingLogic
    {
        //Task<List<Booking>> GetAllBookings();
        //Task<Booking> GetBookingById(int id);
        Task<bool> InsertBooking(Booking booking);
        //Task<bool> UpdateBooking(Booking booking);
        //Task<bool> DeleteBooking(int id);
    }
}
