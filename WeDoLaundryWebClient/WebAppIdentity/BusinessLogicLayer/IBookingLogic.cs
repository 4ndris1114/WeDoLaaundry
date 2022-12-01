using WebAppIdentity.Models;

namespace WebAppIdentity.BusinessLogicLayer
{
    public interface IBookingLogic
    {
<<<<<<< Updated upstream
        Task<List<Booking>> GetAll();
=======
        Task<List<Booking>> GetCustomersBookings(int customerId);
        //Task<List<Booking>> GetAllBookings();
>>>>>>> Stashed changes
        //Task<Booking> GetBookingById(int id);
        Task<bool> InsertBooking(Booking booking);
        //Task<bool> UpdateBooking(Booking booking);
        //Task<bool> DeleteBooking(int id);
    }
}
