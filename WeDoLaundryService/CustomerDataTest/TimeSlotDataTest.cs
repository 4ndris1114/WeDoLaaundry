using Data.Database_layer;
using Data.Model_layer;
using DataAccess;
using DataAccess.Database_layer;
using Model.Model_layer;
using System;
using System.Collections.Generic;
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
        public void testIncreaseAvailability()
        {
            //Arrange
            var date = new DateOnly(2000, 1, 1);
            TimeSlot newSlot = new(date, "19-21", 1);

            //Act
            bool wasUpdated = _timeSlotDataAccess.IncreateAvailability(newSlot);
            int updatedAvailability = _timeSlotDataAccess.getTimeSlot().Availability;

            //Assert
            Assert.True(wasUpdated);
            Assert.True(updatedAvailability = 0);
        }

        [Fact]
        public void testGetAll()
        {
            //Arrange

            //Act
            List<Booking> foundBookings = _bookingDatabaseAccess.GetAll();
            extraOutput.WriteLine("Found bookings: " + foundBookings.Count);

            //Assert
            Assert.True(foundBookings.Count > 0);
        }

    }
}
