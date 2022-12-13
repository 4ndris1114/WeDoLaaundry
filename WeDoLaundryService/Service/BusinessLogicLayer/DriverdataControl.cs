using Data.Database_layer;
using Data.Model_layer;
using DataAccess;

namespace Service.BusinessLogicLayer
{
    public class DriverdataControl : IDriverdata
    {

        IDriverAccess _driverAccess;

        public DriverdataControl(IConfiguration config)
        {
            _driverAccess = new DriverDatabaseAccess(config);
        }

        public DriverdataControl()
        {
        }

        public List<Driver>? Get()
        {
            List<Driver>? foundDrivers = null;
            try
            {
                foundDrivers = _driverAccess.GetAllDrivers();
            }
            catch
            {

                foundDrivers = null;
            }
            return foundDrivers;
        }

        public Driver? GetById(int id)
        {
            Driver? foundDriver;
            try
            {
                foundDriver = _driverAccess.GetById(id);
            }
            catch
            {

                foundDriver = null;
            }
            return foundDriver;
        }

        public Driver? GetByUserId(string userId)
        {
            Driver? foundDriver;
            try
            {
                foundDriver = _driverAccess.GetByUserId(userId);
            }
            catch
            {

                foundDriver = null;
            }
            return foundDriver;
        }

        public int Add(Driver driver)
        {
            int insertedId;
            try
            {
                insertedId = _driverAccess.CreateDriver(driver);
            }
            catch
            {
                insertedId = -1;
            }
            return insertedId;
        }

        public bool Update(Driver driver)
        {
            bool wasUpdated;
            try
            {
                wasUpdated = _driverAccess.UpdateDriver(driver);
            }
            catch
            {
                wasUpdated = false;
            }
            return wasUpdated;
        }

        public bool Delete(int id)
        {
            bool wasDeleted;
            try
            {
                wasDeleted = _driverAccess.DeleteDriver(id);
            }
            catch
            {
                wasDeleted = false;
            }
            return wasDeleted;
        }
    }
}
