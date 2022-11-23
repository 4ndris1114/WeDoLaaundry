using Data.Model_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model_layer
{
    public class Booking
    {
        public enum Status 
        {
            BOOKED = 0,
            IN_PROGRESS = 1,
            RETURNED = 2
        }


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

        public Booking(int id, Customer customer, int driverId, DateTime pickUpTime, DateTime returnTime, string pickUpAddress, string returnAddress, Status bookingStatus, int amountOfBags, int invoiceId)
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

        public Booking(Customer customer, int driverId, DateTime pickUpTime, DateTime returnTime, string pickUpAddress, string returnAddress, Status bookingStatus, int amountOfBags, int invoiceId)
        {
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

        public Booking()
        {

        }
    }
}
