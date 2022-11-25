using Model.Model_layer;

namespace DataAccess.Database_layer
{
    public interface ITimeslotDatabaseAccess
    {
        bool DecreaseAvailability(TimeSlot timeslot);
        List<TimeSlot> GetAll();
    }
}