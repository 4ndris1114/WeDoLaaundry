using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppIdentity.BusinessLogicLayer;
using WebAppIdentity.Models;

namespace WebAppIdentity.Areas.Identity.Pages.Account.Manage
{
    public class CurrentOrderModel : PageModel
    {
        public List<Booking> BookingList { get; set; }

        public List<string> CollectionTimeStrings { get; set; }

        public List<string> DeliveryTimeStrings { get; set; }

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICustomerLogic _customerLogic;
        private readonly IBookingLogic _bookingLogic;
        private readonly ITimeSlotLogic _timeSlotLogic;

        public CurrentOrderModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _customerLogic = new CustomerLogic();
            _bookingLogic = new BookingLogic();
            _timeSlotLogic = new TimeSlotLogic();
        }

        [HttpGet]
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load booking history of user with ID: '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var tempCustomer = await _customerLogic.GetCustomerByUserId(user.Id);
            BookingList = await _bookingLogic.GetCustomersBookings(tempCustomer.Id);
            if (BookingList != null)
            {
                BookingList.RemoveAll(booking => booking.Status == Booking.BookingStatus.RETURNED);
                foreach (var booking in BookingList)
                {
                    if (booking.Status == Booking.BookingStatus.BOOKED || booking.Status == Booking.BookingStatus.IN_PROGRESS)
                    {
                        TimeSlot collectionTime = await _timeSlotLogic.GetById(booking.PickUpTimeId);
                        TimeSlot deliveryTime = await _timeSlotLogic.GetById(booking.ReturnTimeId);
                        CollectionTimeStrings = new();
                        DeliveryTimeStrings = new();
                        CollectionTimeStrings.Add(collectionTime.Date.ToString("dd/MM-yyyy") + "  " + collectionTime.Slot);
                        DeliveryTimeStrings.Add(deliveryTime.Date.ToString("dd/MM-yyyy") + " " + deliveryTime.Slot);
                    }
                }
            }
            ViewData["bookingList"] = BookingList;
        }
    }
}
