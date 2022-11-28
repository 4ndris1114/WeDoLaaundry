using Model.Model_layer;

namespace Service.BusinessLogicLayer
{
    public interface ITimeslotDataControl
    {
        bool DecreaseAvailability(TimeSlot timeslot);
        TimeSlot Get(int id);
        bool Delete(int id);
        int Add(TimeSlot timeslot);
        List<TimeSlot> GetAll();

    }
}