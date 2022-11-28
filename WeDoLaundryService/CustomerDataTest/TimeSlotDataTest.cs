using Data.Database_layer;
using Data.Model_layer;
using DataAccess;
using DataAccess.Database_layer;
using Model.Model_layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
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
            DateTime date = new DateTime(2022, 11, 26); // 2000-01-01
            extraOutput.WriteLine("date: " + date);
            TimeSlot foundSlot = _timeSlotDataAccess.Get(date, "15-18");
            int beforeDecrease = foundSlot.Availability;

            //Act
            bool wasUpdated = _timeSlotDataAccess.DecreaseAvailability(foundSlot);
            int updatedAvailability = _timeSlotDataAccess.Get(date, "15-18").Availability;
            extraOutput.WriteLine("avail: " + updatedAvailability);

            //Assert
            Assert.True(wasUpdated);
            Assert.True(updatedAvailability == beforeDecrease-1);

        }

        //TODO: TEST FOR: GETALL, GET

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
