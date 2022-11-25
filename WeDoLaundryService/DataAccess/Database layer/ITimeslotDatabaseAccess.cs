using Model.Model_layer;
using System.Data.SqlClient;

namespace DataAccess.Database_layer
{
    public interface ITimeslotDatabaseAccess
    {
        bool DecreaseAvailability(TimeSlot timeslot);
        List<TimeSlot> GetAll();
        TimeSlot Get(DateOnly date, String slot);
    }
}

