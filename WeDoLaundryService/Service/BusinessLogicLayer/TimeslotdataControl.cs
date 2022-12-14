using Data.Database_layer;
using Data.Model_layer;
using DataAccess.Database_layer;
using DataAccess.Exceptions;
using Model.Model_layer;

namespace Service.BusinessLogicLayer
{
    public class TimeslotdataControl : ITimeslotDataControl
    {

        ITimeslotDatabaseAccess _timeslotAccess;
        IBookingdataControl _bookingdataControl;

        public TimeslotdataControl(IConfiguration config)
        {
            _timeslotAccess = new TimeslotDatabaseAccess(config);
            _bookingdataControl = new BookingdataControl(config);
        }

        public int Add(TimeSlot timeslot)
        {
            int insertedId;
            try
            {
                insertedId = _timeslotAccess.Create(timeslot);
            }
            catch
            {
                insertedId = -1;
            }
            return insertedId;
        }
        public bool Delete(int id)
        {
            bool wasDeleted;
            try
            {
                wasDeleted = _timeslotAccess.Delete(id);
            }
            catch
            {
                wasDeleted = false;
            }
            return wasDeleted;
        }

        public bool ModifyAvailability(int id, bool mode, int value)
        {
            bool wasUpdated = false;
            try
            {
                if (value > 0)
                {
                    wasUpdated = _timeslotAccess.ModifyAvailability(id, mode, value);
                }
            }
            catch (SlotNotAvailable ex)
            {
                Console.WriteLine(ex.Message);
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

        public TimeSlot? GetByDateAndSlot(DateTime date, string slot)
        {
            TimeSlot foundTimeslot = null;
            try
            {
                foundTimeslot = _timeslotAccess.GetByDateAndSlot(date, slot);
            }
            catch
            {
                foundTimeslot = null;
            }
            return foundTimeslot;
        }

        public List<TimeSlot> GetByDate(DateTime date)
        {
            List<TimeSlot> foundTimeslots = null;
            try
            {
                foundTimeslots = _timeslotAccess.GetByDate(date);
            }
            catch
            {
                foundTimeslots = null;
            }
            return foundTimeslots;
        }

        public List<string> GetAddresses(int id)
        {
            List<string> foundAddresses = null;
            try
            {
                foundAddresses = _bookingdataControl.GetAddressesByTimeslotId(id);
            }
            catch
            {
                foundAddresses = null;
            }
            return foundAddresses;
        }
    }
}
