using Data.Model_layer;
using Service.BusinessLogicLayer;
using Service.Controllers;
using static Model.Model_layer.Booking;

namespace Service.DTOs
{
    public class TimeslotReadDTO
    {
        private CustomerdataControl _customerDataControl;

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Slot { get; set; }
        public int Availability { get; set; }

        public TimeslotReadDTO()
        {

        }

        public TimeslotReadDTO(int id, DateTime date, string slot, int availability)
        {
            Id = id;
            Date = date;
            Slot = slot;
            Availability = availability;
        }
    }
}
