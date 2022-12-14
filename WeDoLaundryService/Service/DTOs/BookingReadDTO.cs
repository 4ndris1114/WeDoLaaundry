using Data.Model_layer;
using Service.BusinessLogicLayer;
using Service.Controllers;
using static Model.Model_layer.Booking;

namespace Service.DTOs
{
    public class BookingReadDTO
    {
        private CustomerdataControl _customerDataControl;

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int DriverId { get; set; }
        public int PickUpTimeId { get; set; }
        public int ReturnTimeId { get; set; }
        public string PickUpAddress { get; set; }
        public string ReturnAddress { get; set; }
        public Status Status { get; set; }
        public int AmountOfBags { get; set; }
        public int InvoiceId { get; set; }

        public BookingReadDTO()
        {

        }

        public BookingReadDTO(int id, int customerId, int driverId, int pickUpTimeId, int returnTimeId, string pickUpAddress, string returnAddress, int bookingStatus, int amountOfBags, int invoiceId)
        {
            _customerDataControl = new CustomerdataControl();
            Id = id;
            CustomerId = customerId;
            DriverId = driverId;
            PickUpTimeId = pickUpTimeId;
            ReturnTimeId = returnTimeId;
            PickUpAddress = pickUpAddress;
            ReturnAddress = returnAddress;
            Status = (Status) bookingStatus;
            AmountOfBags = amountOfBags;
            InvoiceId = invoiceId;
        }
    }
}
