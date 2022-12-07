using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppIdentity.Controllers;
using WebAppIdentity.Models;

namespace WebAppIdentity.Areas.Identity.Pages.Account.Manage
{
    public class Index1Model : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CustomersController _customerController;
        public Customer CurrentCustomer;


        public Index1Model(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _customerController = new();
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(int button)
        {

            await _customerController.Choose(button, User);
            

            return RedirectToPage();
        }
    }
}
