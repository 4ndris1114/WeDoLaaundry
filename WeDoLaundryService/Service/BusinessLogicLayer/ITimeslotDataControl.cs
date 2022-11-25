using Model.Model_layer;

namespace Service.BusinessLogicLayer
{
    public interface ITimeslotDataControl
    {
        bool DecreaseAvailability(TimeSlot timeslot);
        TimeSlot Get(DateTime date, string slot);
        List<TimeSlot> GetAll();

    }
}