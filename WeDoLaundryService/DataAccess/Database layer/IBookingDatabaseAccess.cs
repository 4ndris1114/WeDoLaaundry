using Model.Model_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database_layer
{
    public interface IBookingDatabaseAccess
    {

        List<Booking> GetAll();
        Booking Get(int id);
        List<Booking> GetCustomersBookings(int customerId);
        int CreateBooking(Booking newBooking);
        bool DeleteBooking(int id);

    }
}
