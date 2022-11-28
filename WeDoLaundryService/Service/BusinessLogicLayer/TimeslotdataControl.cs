using DataAccess.Database_layer;
using Model.Model_layer;

namespace Service.BusinessLogicLayer
{
    public class TimeslotdataControl : ITimeslotDataControl
    {

        ITimeslotDatabaseAccess _timeslotAccess;

        public TimeslotdataControl(IConfiguration config)
        {
            _timeslotAccess = new TimeslotDatabaseAccess(config);
        }

        public bool DecreaseAvailability(TimeSlot timeslot)
        {
            bool wasUpdated = false;
            try
            {
                wasUpdated = _timeslotAccess.DecreaseAvailability(timeslot);
            }
            catch
            {
                wasUpdated = false;
            }
            return wasUpdated;
        }

        public TimeSlot Get(int id)
        {
            TimeSlot foundTimeslot = null;
            try
            {
                foundTimeslot = _timeslotAccess.Get(id);
            }
            catch
            {
                foundTimeslot = null;
            }
            return foundTimeslot;
        }

        public List<TimeSlot> GetAll()
        {
            List<TimeSlot> foundTimeslots = null;
            try
            {
                foundTimeslots = _timeslotAccess.GetAll();
            }
            catch
            {
                foundTimeslots = null;
            }
            return foundTimeslots;
        }
    }
}
