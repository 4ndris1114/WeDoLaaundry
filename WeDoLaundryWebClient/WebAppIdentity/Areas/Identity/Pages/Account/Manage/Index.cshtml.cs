using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Xml.Linq;
using WebAppIdentity.BusinessLogicLayer;
using WebAppIdentity.Models;
using WebAppIdentity.Controllers;

namespace WebAppIdentity.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICustomerLogic _customerLogic;
        private readonly CustomersController _customerController;
        public Customer CurrentCustomer;


        public IndexModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _customerLogic = new CustomerLogic();
            CurrentCustomer = new();
            _customerController = new();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "New first name")]
            public string? NewFirstName { get; set; }

            [Display(Name = "New last name")]
            public string? NewLastName { get; set; }

            [Phone]
            [Display(Name = "New phone number")]
            public string? NewPhone { get; set; }

            [Display(Name = "New postal code")]
            public int NewPostalCode { get; set; }

            [Display(Name = "New city")]
            public string? NewCity { get; set; }

            [Display(Name = "New address")]
            public string? NewAddress { get; set; }

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        private async Task LoadAsync(IdentityUser user)
        {
            CurrentCustomer = await _customerLogic.GetCustomerByUserId(user.Id);

            ViewData["CurrentCustomer"] = CurrentCustomer;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            CurrentCustomer = await _customerLogic.GetCustomerByUserId(user.Id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.NewFirstName != CurrentCustomer.FirstName || Input.NewLastName != CurrentCustomer.LastName || Input.NewPhone != CurrentCustomer.Phone || 
                Input.NewPostalCode != CurrentCustomer.PostalCode || Input.NewCity != CurrentCustomer.City || Input.NewAddress != CurrentCustomer.Address)
            {
                var customerToPost = new Customer(CurrentCustomer.Id, Input.NewFirstName, Input.NewLastName, Input.NewPhone, CurrentCustomer.Email, Input.NewPostalCode, Input.NewCity, Input.NewAddress, CurrentCustomer.CustomerType, CurrentCustomer.UserId);

                _customerController.Update(customerToPost, User);
            }

            return RedirectToPage();
        }

    }

}
