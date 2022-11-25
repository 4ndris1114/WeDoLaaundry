using Data.Database_layer;
using Data.Model_layer;
using DataAccess;
using DataAccess.Database_layer;
using Model.Model_layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Tests
{
    public class TimeSlotDataTest
    {
        private readonly ITestOutputHelper extraOutput;
        private readonly ITimeslotDatabaseAccess _timeSlotDataAccess;
        private readonly string _connectionString = "Server=hildur.ucn.dk,1433;Database=CSC-CSD-S211_10407554;User Id = CSC-CSD-S211_10407554; Password=Password1!";

        public TimeSlotDataTest(ITestOutputHelper extraOutput)
        {
            this.extraOutput = extraOutput;
            _timeSlotDataAccess = new TimeslotDatabaseAccess(_connectionString);
        }

        [Fact]
        public void testDecreaseAvailability()
        {
            //Arrange
            var date = new DateOnly(2023, 1, 1); // 2000-01-01
            TimeSlot foundSlot = _timeSlotDataAccess.Get(date, "19-21");
            int beforeDecrease = foundSlot.Availability;

            //Act
            bool wasUpdated = _timeSlotDataAccess.DecreaseAvailability(foundSlot);
            int updatedAvailability = _timeSlotDataAccess.Get(date, "19-21").Availability;
            extraOutput.WriteLine("avail: " + updatedAvailability);

            //Assert
            Assert.True(wasUpdated);
            Assert.True(updatedAvailability == beforeDecrease-1);

        }

        //[Fact]
        //public void testGetAll()
        //{
        //    //Arrange

        //    //Act
        //    List<Booking> foundBookings = _bookingDatabaseAccess.GetAll();
        //    extraOutput.WriteLine("Found bookings: " + foundBookings.Count);

        //    //Assert
        //    Assert.True(foundBookings.Count > 0);
        //}

    }
}
