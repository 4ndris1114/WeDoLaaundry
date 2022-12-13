using Data.Database_layer;
using Data.Model_layer;
using DataAccess;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using Xunit.Abstractions;

namespace DriverDataTest
{
    public class DriverDataTest : IDisposable
    {

        private readonly ITestOutputHelper extraOutput;
        private readonly IDriverAccess _driverAccess;
        private readonly MemoryStream _stream;
        private readonly int insertId;
        private readonly Driver newDriver;

        public DriverDataTest(ITestOutputHelper extraOutput)
        {
            var config = InitConfiguration();
            this.extraOutput = extraOutput;
            _stream = new MemoryStream();

            _driverAccess = new DriverDatabaseAccess(config);
            newDriver = new Driver(1, "Test", "Test", "+4555222232", 1234, "TestCity",
                "Test street test", "test@example.com", 1000);
            insertId = _driverAccess.CreateDriver(newDriver);
            newDriver.Id = insertId;
        }

        private IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        public void Dispose()
        {
            _stream.Dispose();
            _driverAccess.DeleteDriver(insertId);

        }

        [Fact]
        public void testGetAllDriver()
        {
            //Arrange

            //Act
            List<Driver> foundDrivers = _driverAccess.GetAllDrivers();
            extraOutput.WriteLine("Found drivers: " + foundDrivers.Count);

            //Assert
            Assert.True(foundDrivers.Count > 0);
        }

        [Fact]
        public void testGetDriverById()
        {
            //Arrange

            //Act
            Driver foundDriver = _driverAccess.GetById(newDriver.Id);
            extraOutput.WriteLine("Found driver: " + foundDriver.Id);

            //Assert
            Assert.True(foundDriver != null);
        }

        [Fact]
        public void testCreateDriver()
        {
            //Arrange

            //Act
            int insertId = _driverAccess.CreateDriver(newDriver);
            extraOutput.WriteLine("Generated key (id): " + insertId);
            _driverAccess.DeleteDriver(insertId);

            //Assert
            Assert.True(insertId != -1);
        }

        [Fact]
        public void testUpdateDriver()
        {
            //Arrange
            string originalValue = newDriver.FirstName;
            string updatedValue = "Test"; // new value to be checked
            Driver originalDriver = newDriver;

            //Act
            int insertedId = _driverAccess.CreateDriver(originalDriver);
            bool updateReturnedTrue = _driverAccess.UpdateDriver(newDriver);

            Driver retrievedDriver = _driverAccess.GetById(insertId);

            extraOutput.WriteLine("Original value: " + originalValue);

            //remove customer
            bool wasDeleted = _driverAccess.DeleteDriver(insertedId);

            //Assert
            Assert.True(updateReturnedTrue);
            Assert.True(wasDeleted);
        }
    }
}