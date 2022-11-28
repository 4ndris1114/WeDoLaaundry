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
    public class BookingDataTest : IDisposable
    {
        private readonly ITestOutputHelper extraOutput;
        private readonly ICustomerAccess _customerAccess;
        private readonly MemoryStream _stream;
        private readonly IBookingDatabaseAccess _bookingDatabaseAccess;
        private readonly ITimeslotDatabaseAccess _timeslotDatabaseAccess;
        private readonly string _connectionString = "Server=hildur.ucn.dk,1433;Database=CSC-CSD-S211_10407554;User Id = CSC-CSD-S211_10407554; Password=Password1!";

        public BookingDataTest(ITestOutputHelper extraOutput)
        {
            this.extraOutput = extraOutput;
            _stream = new MemoryStream();
            _customerAccess = new CustomerDatabaseAccess(_connectionString);
            _bookingDatabaseAccess = new BookingDatabaseAccess(_connectionString);
            _timeslotDatabaseAccess = new TimeslotDatabaseAccess(_connectionString);
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        [Fact]
        public void testCreateBooking()
        {
            //Arrange
            Booking newBooking = new(_customerAccess.GetById(1010), 12, _timeslotDatabaseAccess.Get(1001), _timeslotDatabaseAccess.Get(1002), "pickupaddress", "returnaddress", Booking.Status.BOOKED, 2, 12);

            //Act
            int insertedId = _bookingDatabaseAccess.CreateBooking(newBooking);
            extraOutput.WriteLine("Generated key (id): " + insertedId);

            //Assert
            Assert.True(insertedId > 0);
        }

        [Fact]
        public void testGetAllBookings()
        {
            //Arrange

            //Act
            List<Booking> foundBookings = _bookingDatabaseAccess.GetAll();
            extraOutput.WriteLine("Found bookings: " + foundBookings.Count);

            //Assert
            Assert.True(foundBookings.Count > 0);
        }

        [Fact]
        public void testGetBookingById()
        {
            //Arrange
            Booking newBooking = new(_customerAccess.GetById(1010), 12, _timeslotDatabaseAccess.Get(1001), _timeslotDatabaseAccess.Get(1002), "pickupaddress", "returnaddress", Booking.Status.BOOKED, 2, 12);
            string pickupAddress = newBooking.PickUpAddress;

            //Act
            int insertedId = _bookingDatabaseAccess.CreateBooking(newBooking);
            string foundPickupAddress = _bookingDatabaseAccess.Get(insertedId).PickUpAddress;

            //Assert
            Assert.True(pickupAddress == foundPickupAddress);
        }
    }
}