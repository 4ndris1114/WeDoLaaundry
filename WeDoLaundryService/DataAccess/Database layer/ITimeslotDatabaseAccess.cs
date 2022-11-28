using Model.Model_layer;
using System.Data.SqlClient;

namespace DataAccess.Database_layer
{
    public interface ITimeslotDatabaseAccess
    {
        bool DecreaseAvailability(int id);
        List<TimeSlot> GetAll();
        TimeSlot Get(int id);
        int Create(TimeSlot timeslot);
        bool Delete(int id);

    }
}

