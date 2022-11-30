using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using WebAppIdentity.BusinessLogicLayer;
//using WebAppIdentity.Models;

namespace WebAppIdentity.Areas.Identity.Pages.Account.Manage
{

    public class OrdersModel : PageModel
    {
        //public IBookingLogic _bookingLogic;
        //public  List<Booking> bookings;

        public async Task OnGetAsync()
        {
            //_bookingLogic = new BookingLogic();
            //List<Booking> bookings = await _bookingLogic.GetAllBookings();
        }

        //    public MyEnumerator GetEnumerator()
        //    {
        //        return new MyEnumerator(this);
        //    }
        //}
        //public class MyEnumerator
        //{
        //    int nIndex;
        //    OrdersModel collection;
        //    public MyEnumerator(OrdersModel coll)
        //    {
        //        collection = coll;
        //        nIndex = -1;
        //    }

        //    public bool MoveNext()
        //    {
        //        nIndex++;
        //        return (nIndex < collection.bookings.Count); /////////////////////////////
        //    }

        //    public Booking Current => collection.bookings[nIndex];
        //}
    }
}