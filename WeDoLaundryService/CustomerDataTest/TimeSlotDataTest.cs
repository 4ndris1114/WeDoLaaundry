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
    public class TimeSlotDataTest : IDisposable
    {
        private readonly ITestOutputHelper extraOutput;
        private readonly ITimeslotDatabaseAccess _timeSlotDataAccess;
        private readonly MemoryStream _stream;
        private readonly string _connectionString = "Server=hildur.ucn.dk,1433;Database=CSC-CSD-S211_10407554;User Id = CSC-CSD-S211_10407554; Password=Password1!";
        private readonly TimeSlot newSlot;
        private readonly int insertedId;

        public TimeSlotDataTest(ITestOutputHelper extraOutput)
        {
            this.extraOutput = extraOutput;
            _stream = new MemoryStream();
            _timeSlotDataAccess = new TimeslotDatabaseAccess(_connectionString);
            newSlot = new(-1,new DateTime(2023, 05, 05),"15-18",10);
            int insertedId = _timeSlotDataAccess.Create(newSlot);
            //newSlot.Id = insertedId;
        }

        public void Dispose()
        {
            _stream.Dispose();
            _timeSlotDataAccess.Delete(insertedId);
        }

        [Fact]
        public void TestDecreaseAvailability()
        {
            //Arrange
            int beforeDecrease = newSlot.Availability;

            //Act
            bool wasUpdated = _timeSlotDataAccess.DecreaseAvailability(insertedId);
            int updatedAvailability = _timeSlotDataAccess.Get(insertedId).Availability;
            extraOutput.WriteLine("avail: " + updatedAvailability);

            //Assert
            Assert.True(wasUpdated);
            Assert.True(updatedAvailability == beforeDecrease-1);

        }

        [Fact]
        public void TestGetAll()
        {
            //Arrange

            //Act
            List<TimeSlot> foundSlots = _timeSlotDataAccess.GetAll();
            extraOutput.WriteLine("Found timeslots: " + foundSlots.Count);

            //Assert
            Assert.True(foundSlots.Count > 0);
        }

        [Fact]
        public void TestCreateTimeslot()
        {
            //Arrange

            //Act
            int insertId = _timeSlotDataAccess.Create(newSlot);
            extraOutput.WriteLine("Generated key (id): " + insertId);
            _timeSlotDataAccess.Delete(insertId);

            //Assert
            Assert.True(insertId != -1);
        }

        [Fact]
        public void TestGetTimeslotById()
        {
            //Arrange

            //Act
            TimeSlot foundTimeslot = _timeSlotDataAccess.Get(insertedId);
            extraOutput.WriteLine("Found timeslot: " + foundTimeslot.Id);

            //Assert
            Assert.True(foundTimeslot != null);
        }

        [Fact]
        public void TestDeleteTimeslot()
        {
            //arrange

            //act

            //assert

        }

    }
}
