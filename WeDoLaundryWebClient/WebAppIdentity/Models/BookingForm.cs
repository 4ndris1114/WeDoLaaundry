using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppIdentity.Models
{
    public class BookingForm
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public int CustomerId { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public int DriverId { get; set; } = 0;

        [DisplayName("Select day")]
        [Required(ErrorMessage = "Collection time is required")]
        public int PickUpDay { get; set; }

        [DisplayName("Select day")]
        [Required(ErrorMessage = "Delivery time is required")]
        public int ReturnDay { get; set; }

        //[DisplayName("Select hour")]
        //public string PickUpTimeSlot { get; set; }

        //[DisplayName("Select hour")]
        //public string ReturnTimeSlot { get; set; }

        [DisplayName("Collection address")]
        [Required(ErrorMessage = "Collection address is required")]
        [DataType(DataType.Text)]
        public string PickUpAddress { get; set; }


        [DisplayName("Delivery address")]
        [Required(ErrorMessage = "Delivery address is required")]
        [DataType(DataType.Text)]
        public string ReturnAddress { get; set; }

        public int Status { get; set; } = 0;

        [DisplayName("Amount of bags")]
        [Required(ErrorMessage = "Amount of bags is required")]
        [DataType(DataType.Text)]
        public int AmountOfBags { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public int InvoiceId { get; set; } = 0;

    }
}
