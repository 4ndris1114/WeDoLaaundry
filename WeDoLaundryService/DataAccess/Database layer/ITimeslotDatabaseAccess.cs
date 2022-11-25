using Model.Model_layer;

namespace DataAccess.Database_layer
{
    public interface ITimeslotDatabaseAccess
    {
        bool IncreaseAvailiability();
        List<TimeSlot> GetAll();
    }
}