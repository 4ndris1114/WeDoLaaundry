
using NuGet.Protocol;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace WebAppIdentity.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("First name")]
        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression("^(?=.{1,40}$)[a-zA-Z]+(?:[-'\\s][a-zA-Z]+)*$")]
        [StringLength(50, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression("^(?=.{1,40}$)[a-zA-Z]+(?:[-'\\s][a-zA-Z]+)*$")]
        [StringLength(50, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [DisplayName("Phone number")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("[+][0-9]{8,20}")] //Regex expression for format to be like: +[country code] [number]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(256)]
        public string Email { get; set; } = "";

        [DisplayName("Postal code")]
        [Required(ErrorMessage = "Postal code is required.")]
        [IntegerValidator(MinValue = 100, MaxValue = 99999)]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "City is required.")]
        [RegularExpression("^(?=.{1,40}$)[a-zA-Z]+(?:[-'][a-zA-Z]+)*$")]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required.")]
        [RegularExpression("^[.0-9a-zA-Z\\s,-]+$")]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [DisplayName("Customer type")]
        [Required(ErrorMessage = "Customer type is required.")]
        [DataType(DataType.Text)]
        public int CustomerType  { get; set; }

        [ForeignKey("FK_CustomerUser")]
        [DataType(DataType.Text)]
        [StringLength(450)]
        public string UserId { get; set; } = "";
    }
}
