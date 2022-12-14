using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; }
        public int CustomerId { get; set; }
        public int DriverId { get; set; }
        public string PickUpSlot { get; set; }
        public int PickUpTimeId { get; set; }
        public string ReturnSlot { get; set; }
        public int ReturnTimeId { get; set; }
        private string pickUpAddress;

        public string PickUpAddress
        {
            get { return pickUpAddress; }
            set { pickUpAddress = value; NotifyPropertyChanged("PickUpAddress"); }
        }

        private string returnAddress;
        public string ReturnAddress
        {
            get { return returnAddress; }
            set { returnAddress = value; NotifyPropertyChanged("ReturnAddress"); }
        }
        private BookingStatus status;
        public BookingStatus Status
        {
            get { return status; }
            set { status = value; NotifyPropertyChanged("Status"); }
        }
        private int amountOfBags;
        public int AmountOfBags
        {
            get { return amountOfBags; }
            set { amountOfBags = value; NotifyPropertyChanged("AmountOfBags"); }
        }
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public BookingViewModel(Booking booking, string pickUpSlot, string returnSlot)
        {
            Id = booking.Id;
            CustomerId = booking.CustomerId;
            //DriverId = driverId;
            PickUpSlot = pickUpSlot;
            PickUpTimeId = booking.PickUpTimeId;
            ReturnTimeId = booking.ReturnTimeId;
            ReturnSlot = returnSlot;
            PickUpAddress = booking.PickUpAddress;
            ReturnAddress = booking.ReturnAddress;
            Status = (BookingStatus)booking.Status;
            AmountOfBags = booking.AmountOfBags;
        }
    }
}
