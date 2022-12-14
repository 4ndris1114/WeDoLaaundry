using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.ServiceAccess;

namespace WpfApp1.Controller_layer
{
    public class DriversController
    {
        DriverServiceAccess  _serviceAccess;

        public DriversController()
        {
            _serviceAccess = new DriverServiceAccess();
        }

        public async Task<List<Driver>> GetDriversAsync()
        {
            List<Driver> foundDrivers = null;

            foundDrivers = await _serviceAccess.GetDriversAsync();


            return foundDrivers;
        }

        public async Task<bool> UpdateDriverAsync(int id, Driver driver)
        {
            bool wasUpdated = await _serviceAccess.UpdateDriverAsync(id, driver);

            return wasUpdated;
        }

        public async Task<bool> DeleteDriverAsync(int id)
        {
            bool wasDeleted = await _serviceAccess.DeleteDriverAsync(id);

            return wasDeleted;
        }
    }
}
