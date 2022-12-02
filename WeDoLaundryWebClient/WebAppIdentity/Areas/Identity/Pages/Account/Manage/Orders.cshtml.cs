using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebAppIdentity.BusinessLogicLayer;
using WebAppIdentity.Models;

namespace WebAppIdentity.Areas.Identity.Pages.Account.Manage
{
    public class OrdersModel : PageModel
    {
        public List<Booking> BookingList { get; set; }

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICustomerLogic _customerLogic;
        private readonly IBookingLogic _bookingLogic;

        public OrdersModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _customerLogic = new CustomerLogic();
            _bookingLogic = new BookingLogic();
        }

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

            ViewData["bookingList"] = BookingList;
        }
    }
}
