using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Booking
    {
        public enum BookingStatus
        {
            BOOKED = 0,
            IN_PROGRESS = 1,
            RETURNED = 2
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int DriverId { get; set; }
        public int PickUpTimeId { get; set; }
        public int ReturnTimeId { get; set; }
        public string PickUpAddress { get; set; }
        public string ReturnAddress { get; set; }
        public BookingStatus Status { get; set; }
        public int AmountOfBags { get; set; }

        public Booking() { }

        public Booking(int id, int customerId,/* int DriverId,*/ int pickUpTimeId, int returnTimeId, string pickUpAddress, string returnAddress, int status, int amountOfBags)
        {
            Id = id;
            CustomerId = customerId;
            //DriverId = driverId;
            PickUpTimeId = pickUpTimeId;
            ReturnTimeId = returnTimeId;
            PickUpAddress = pickUpAddress;
            ReturnAddress = returnAddress;
            Status = (BookingStatus)status;
            AmountOfBags = amountOfBags;
        }
    }
}
