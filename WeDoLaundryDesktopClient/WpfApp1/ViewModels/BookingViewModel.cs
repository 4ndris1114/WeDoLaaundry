using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.ViewModels
{
    public enum BookingStatus
    {
        BOOKED = 0,
        IN_PROGRESS = 1,
        RETURNED = 2
    }
    public class BookingViewModel
    {
        public int Id { get; }
        public int CustomerId { get; set; }
        public int DriverId { get; set; }
        public int PickUpTimeId { get; set; }
        public int ReturnTimeId { get; set; }
        public string PickUpAddress { get; set; }
        public string ReturnAddress { get; set; }
        public BookingStatus Status { get; set; }
        public int AmountOfBags { get; set; }

        public BookingViewModel(Booking booking)
        {
            Id = booking.Id;
            CustomerId = booking.CustomerId;
            //DriverId = driverId;
            PickUpTimeId = booking.PickUpTimeId;
            ReturnTimeId = booking.ReturnTimeId;
            PickUpAddress = booking.PickUpAddress;
            ReturnAddress = booking.ReturnAddress;
            Status = (BookingStatus)booking.Status;
            AmountOfBags = booking.AmountOfBags;
        }
    }
}
