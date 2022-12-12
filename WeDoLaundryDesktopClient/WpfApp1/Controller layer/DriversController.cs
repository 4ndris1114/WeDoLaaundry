using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Model_layer;
using WpfApp1.Service_layer;
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

        public Task<List<Driver>> GetDriversAsync()
        {
            return GetDriversAsync(_serviceAccess);
        }

        public async Task<List<Driver>> GetDriversAsync(DriverServiceAccess _serviceAccess)
        {
            List<Driver> foundDrivers = null;

            foundDrivers = await _serviceAccess.GetAll();

            return foundDrivers;

        }

        public async Task<List<Driver>> GetOrdersAsync(int orderId)
        {
            List<Driver> foundDrivers = null;

            //foundDrivers = await _serviceAccess.GetOrdersAsync(orderId);

            return foundDrivers;

        }
    }
}
