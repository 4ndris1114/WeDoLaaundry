using Data.Database_layer;
using Data.Model_layer;
using DataAccess;
using DataAccess.Database_layer;
using Microsoft.Extensions.Configuration;
using Model.Model_layer;
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

        public BookingDataTest(ITestOutputHelper extraOutput)
        {
            var config = InitConfiguration();
            this.extraOutput = extraOutput;
            _stream = new MemoryStream();

            _customerAccess = new CustomerDatabaseAccess(config);
            _bookingDatabaseAccess = new BookingDatabaseAccess(config);
            _timeslotDatabaseAccess = new TimeslotDatabaseAccess(config);
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
        public void testCreateBookingConcurrently()
        {
            //Arrange
            int insert1Id = _timeslotDatabaseAccess.Create(new TimeSlot(-1, new DateTime(2022, 12, 02), "15-18", 1));
            int insert2Id = _timeslotDatabaseAccess.Create(new TimeSlot(-1, new DateTime(2022, 12, 02), "18-21", 1));
            Booking newBooking1 = new(_customerAccess.GetById(1010), 12, _timeslotDatabaseAccess.Get(insert1Id), _timeslotDatabaseAccess.Get(insert2Id), "1addressCollection", "1addressReturn", Booking.Status.BOOKED, 2, 1);

            Booking newBooking2 = new(_customerAccess.GetById(1010), 12, _timeslotDatabaseAccess.Get(insert1Id), _timeslotDatabaseAccess.Get(insert2Id), "2addressCollection", "2addressReturn", Booking.Status.BOOKED, 2, 3);
            //Act
            int insertedId1 = _bookingDatabaseAccess.CreateBooking(newBooking1);
            extraOutput.WriteLine("Generated key newBooking1 (id): " + insertedId1);

            Thread.Sleep(500);

            int insertedId2 = _bookingDatabaseAccess.CreateBooking(newBooking2);
            extraOutput.WriteLine("Generated key newBooking2 (id): " + insertedId2);

            //Assert
            Assert.True(insertedId1 > 0);
            Assert.True(insertedId2 == -1);
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