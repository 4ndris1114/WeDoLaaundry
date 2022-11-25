using Data.Model_layer;
using Service.BusinessLogicLayer;
using Service.Controllers;
using static Model.Model_layer.Booking;

namespace Service.DTOs
{
    public class TimeslotReadDTO
    {
        private CustomerdataControl _customerDataControl;

        public DateTime Date { get; set; }
        public string Slot { get; set; }
        public int Availability { get; set; }

        public TimeslotReadDTO()
        {

        }

        public TimeslotReadDTO(DateTime date, string slot, int availability)
        {
            Date = date;
            Slot = slot;
            Availability = availability;
        }
    }
}
