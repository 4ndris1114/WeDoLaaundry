using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WebAppIdentity.BusinessLogicLayer;
using WebAppIdentity.Models;

namespace WebAppIdentity.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICustomerLogic _customerLogic;


        public IndexModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _customerLogic = new CustomerLogic();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public class InputModel
        {
            [Required]

            [Display(Name = "New first name")]
            public string? FirstName { get; set; }

            [Required]
            [Display(Name = "New last name")]
            public string? LastName { get; set; }

            [Required]
            [Phone]
            [Display(Name = "New phone number")]
            public string? Phone { get; set; }

            [Required]
            [Display(Name = "New phone number")]
            public int PostalCode { get; set; }

            [Required]
            [Display(Name = "New phone number")]
            public string? City { get; set; }

            [Required]
            [Display(Name = "New phone number")]
            public string? Address { get; set; }
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
            var currentCustomer = await _customerLogic.GetCustomerByUserId(user.Id);

            Input = new InputModel
            {
                FirstName = currentCustomer.FirstName,
                LastName = currentCustomer.LastName,
                Phone = currentCustomer.Phone,
                PostalCode = currentCustomer.PostalCode,
                City = currentCustomer.City,
                Address = currentCustomer.Address
            };

            ViewData["currentCustomer"] = currentCustomer;
        }


    }


}
