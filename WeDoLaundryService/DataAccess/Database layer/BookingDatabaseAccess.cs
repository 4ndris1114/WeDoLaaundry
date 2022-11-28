using Data.Database_layer;
using Data.Model_layer;
using Microsoft.Extensions.Configuration;
using Model.Model_layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Model_layer.Booking;

namespace DataAccess.Database_layer
{
    public class BookingDatabaseAccess : IBookingDatabaseAccess
    {
        private readonly string _connectionString;
        private readonly ICustomerAccess _customerAccess;
        private readonly ITimeslotDatabaseAccess _timeslotAccess;

        public BookingDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("WeDoLaundry");
            _customerAccess = new CustomerDatabaseAccess(configuration);
        }

        public BookingDatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
            _customerAccess = new CustomerDatabaseAccess(_connectionString);
            _timeslotAccess = new TimeslotDatabaseAccess(_connectionString);
        }

        public int CreateBooking(Booking newBooking)
        {
            int insertedId = -1;

            string insertString = "INSERT INTO Bookings(customerId, driverId, pickUpTimeId, returnTimeId, pickUpAddress, returnAddress, status, noOfBags, invoiceId) " +
                "OUTPUT INSERTED.ID VALUES(@CustomerId, @DriverId, @PickupTime, @ReturnTime, @PickupAddress, @ReturnAddress, @Status, @AmountOfBags, @InvoiceId)";

            using (SqlConnection con = new(_connectionString))
                using (SqlCommand cmd = new(insertString, con))
                {
                    con.Open();

                    //Assign parameters
                    SqlParameter customerIdParam = new("@CustomerId", newBooking.Customer.GetId());
                    cmd.Parameters.Add(customerIdParam);
                    SqlParameter driverIdParam = new("@DriverId", newBooking.DriverId);
                    cmd.Parameters.Add(driverIdParam);
                    SqlParameter pickupTimeParam = new("@PickupTime", newBooking.PickUpTime.Id);
                    cmd.Parameters.Add(pickupTimeParam);
                    SqlParameter returnTimeParam = new("@ReturnTime", newBooking.ReturnTime.Id);
                    cmd.Parameters.Add(returnTimeParam);
                    SqlParameter pickupAddressParam = new("@PickupAddress", newBooking.PickUpAddress);
                    cmd.Parameters.Add(pickupAddressParam);
                    SqlParameter returnAddressParam = new("@ReturnAddress", newBooking.ReturnAddress);
                    cmd.Parameters.Add(returnAddressParam);
                    SqlParameter statusParam = new("@Status", newBooking.BookingStatus);
                    cmd.Parameters.Add(statusParam);
                    SqlParameter amountOfBagsParam = new("@AmountOfBags", newBooking.AmountOfBags);
                    cmd.Parameters.Add(amountOfBagsParam);
                    SqlParameter invoiceIdParam = new("@InvoiceId", newBooking.InvoiceId);
                    cmd.Parameters.Add(invoiceIdParam);


                    insertedId = (int) cmd.ExecuteScalar();

                    con.Close();
            }
            return insertedId;
        }

        public Booking Get(int id)
        {
            Booking booking = new();

            string SQL_string = "SELECT * FROM Bookings WHERE id = @Id";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter idParam = new("@Id", id);
                command.Parameters.Add(idParam);


                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    booking = GetBookingReader(reader);
                }
                con.Close();
            }

            return booking;
        }

        public List<Booking> GetAll()
        {
            List<Booking>? returnList = new();

            string SQL_string = "SELECT * FROM Bookings";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                returnList = new();
                while (reader.Read())
                {
                    returnList.Add(GetBookingReader(reader));
                }
                con.Close();
            }
            return returnList;
        }

        private Booking GetBookingReader(SqlDataReader reader)
        {
            Booking returnBooking;
            int tempId = reader.GetInt32(reader.GetOrdinal("id"));
            Customer customer = new();
            if (!reader.IsDBNull(1))
            {
                customer = _customerAccess.GetById(reader.GetInt32(reader.GetOrdinal("customerId")));
            }
            int driverId = 0;
            if (!reader.IsDBNull(2))
            {
                reader.GetInt32(reader.GetOrdinal("driverId")); //change later to object
            }
            TimeSlot pickupTime = _timeslotAccess.Get(reader.GetInt32(reader.GetOrdinal("pickUpTimeId")));
            TimeSlot returnTime = _timeslotAccess.Get(reader.GetInt32(reader.GetOrdinal("returnTimeId")));
            string pickupAddress = reader.GetString(reader.GetOrdinal("pickUpAddress"));
            string returnAddress = reader.GetString(reader.GetOrdinal("returnAddress"));
            Status status = (Status) Enum.Parse(typeof(Status), reader.GetString(reader.GetOrdinal("status")).ToUpper(), true);
            int amountOfBags = reader.GetInt32(reader.GetOrdinal("noOfBags"));
            int invoiceId = 0;
            if (!reader.IsDBNull(9))
            {
                reader.GetInt32(reader.GetOrdinal("invoiceId")); //change later to object
            }

            returnBooking = new(tempId, customer, driverId, pickupTime, returnTime, pickupAddress, returnAddress, status, amountOfBags, invoiceId);

            return returnBooking;
        }
    }
}
