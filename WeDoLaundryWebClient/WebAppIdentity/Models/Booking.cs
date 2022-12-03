using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebAppIdentity.Models
{
    public class Booking
    {

        public enum BookingStatus
        {
            BOOKED = 0,
            IN_PROGRESS = 1,
            RETURNED = 2
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Fk_CustomerBooking")]
        [DataType(DataType.Text)]
        [Required]
        public int CustomerId { get; set; } 

        [DataType(DataType.Text)]
        [Required]
        public int DriverId { get; set; } = 0;

        [DisplayName("Collection time")]
        [Required(ErrorMessage = "Collection time is required")]
        [DataType(DataType.Text)]
        public int PickUpTimeId { get; set; }

        [DisplayName("Delivery time")]
        [Required(ErrorMessage = "Delivery time is required")]
        [DataType(DataType.Text)]
        public int ReturnTimeId { get; set; }

        [DisplayName("Collection address")]
        [Required(ErrorMessage = "Collection address is required")]
        [DataType(DataType.Text)]
        public string PickUpAddress { get; set; }


        [DisplayName("Delivery address")]
        [Required(ErrorMessage = "Delivery address is required")]
        [DataType(DataType.Text)]
        public string ReturnAddress { get; set; }

        public BookingStatus Status { get; set; }

        [DisplayName("Amount of bags")]
        [Required(ErrorMessage = "Amount of bags is required")]
        [DataType(DataType.Text)]
        public int AmountOfBags { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public int InvoiceId { get; set; } = 0;

        public Booking(int customerId, int pickUpTimeId, int returnTimeId, string pickUpAddress, string returnAddress, int status, int amountOfBags)
        {
            CustomerId = customerId;
            PickUpTimeId = pickUpTimeId;
            ReturnTimeId = returnTimeId;
            PickUpAddress = pickUpAddress;
            ReturnAddress = returnAddress;
            Status = (BookingStatus) status;
            AmountOfBags = amountOfBags;
        }

    }
}
