using Data.Model_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database_layer
{
    public interface IDriverAccess
    {

        Driver GetById(int id);

        Driver GetByUserId(string userId);

        List<Driver> GetAllDrivers();

        int CreateDriver(Driver driver);

        bool UpdateDriver(Driver driver);

        bool DeleteDriver(int id);

    }
}
