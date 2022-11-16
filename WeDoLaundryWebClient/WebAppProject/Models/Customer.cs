
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace WebAppProject.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; }


        [DisplayName("First name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(40, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(40, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [DisplayName("Phone number")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("[+][0-9]{5,20}")] //Regex expression for format to be like: +[country code] [number]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DisplayName("Email address")]
        [Required(ErrorMessage = "Email address is required.")]
        [RegularExpression("[a-z0-9]+[@][a-z]+[.][a-z]+")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Postal code")]
        [Required(ErrorMessage = "Postal code is required.")]
        [IntegerValidator(MinValue = 100, MaxValue = 99999)]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [DisplayName("Address")]
        [StringLength(40, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6)]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords aren't matching.")]
        [Required(ErrorMessage = "Password confirmation is required.")]
        [StringLength(100, MinimumLength = 6)]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [StringLength(40, MinimumLength = 2)]
        [DataType(DataType.PhoneNumber)]
        public string CustomerType { get; set; }

    }
}
