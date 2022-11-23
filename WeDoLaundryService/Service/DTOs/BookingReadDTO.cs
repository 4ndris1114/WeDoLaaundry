using Data.Model_layer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using static Model.Model_layer.Booking;

namespace Service.DTOs
{
    public class BookingReadDTO
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int DriverId { get; set; }
        public DateTime PickUpTime { get; set; }
        public DateTime ReturnTime { get; set; }
        public string PickUpAddress { get; set; }
        public string ReturnAddress { get; set; }
        public Status BookingStatus { get; set; }
        public int AmountOfBags { get; set; }
        public int InvoiceId { get; set; }

        public BookingReadDTO()
        {

        }

        public BookingReadDTO(int id, Customer customer, int driverId, DateTime pickUpTime, DateTime returnTime, string pickUpAddress, string returnAddress, Status bookingStatus, int amountOfBags, int invoiceId)
        {
            Id = id;
            Customer = customer;
            DriverId = driverId;
            PickUpTime = pickUpTime;
            ReturnTime = returnTime;
            PickUpAddress = pickUpAddress;
            ReturnAddress = returnAddress;
            BookingStatus = bookingStatus;
            AmountOfBags = amountOfBags;
            InvoiceId = invoiceId;
        }
    }
}
