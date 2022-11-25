using Data.Model_layer;
using Service.BusinessLogicLayer;
using Service.Controllers;
using static Model.Model_layer.Booking;

namespace Service.DTOs
{
    public class TimeslotDTO
    {
        private CustomerdataControl _customerDataControl;

        public DateOnly Date { get; set; }
        public string Slot { get; set; }
        public int Availability { get; set; }

        public TimeslotDTO()
        {

        }

        public TimeslotDTO(DateOnly date, string slot, int availability)
        {
            Date = date;
            Slot = slot;
            Availability = availability;
        }
    }
}
