using Data.Database_layer;
using Data.Model_layer;
using DataAccess.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Model.Model_layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
            _timeslotAccess = new TimeslotDatabaseAccess(configuration);
        }

        public int CreateBooking(Booking newBooking)
        {
            int insertedId = -1;

            string insertString = "INSERT INTO Bookings(customerId, driverId, pickUpTimeId, returnTimeId, pickUpAddress, returnAddress, status, noOfBags, invoiceId) " +
                "OUTPUT INSERTED.ID VALUES(@CustomerId, @DriverId, @PickupTime, @ReturnTime, @PickupAddress, @ReturnAddress, @Status, @AmountOfBags, @InvoiceId)";

            using (SqlConnection con = new(_connectionString))
                using (SqlCommand cmd = new(insertString, con))
                {

                //Assign parameters
                SqlParameter customerIdParam = new("@CustomerId", newBooking.Customer.Id);
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

                con.Open();

                SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.Serializable);
                cmd.Transaction = transaction;

                try 
                {
                    if (_timeslotAccess.Get(newBooking.PickUpTime.Id).Availability > 0 &&
                        _timeslotAccess.Get(newBooking.ReturnTime.Id).Availability > 0)
                    {
                        _timeslotAccess.ModifyAvailability(newBooking.PickUpTime.Id, false, 1);
                        _timeslotAccess.ModifyAvailability(newBooking.ReturnTime.Id, false, 1);
                    } else
                    {
                        throw new SlotNotAvailable("The slot availability is 0.");
                    }
                    insertedId = (int)cmd.ExecuteScalar();

                    transaction.Commit();
                }
                catch (Exception ex) 
                {
                    transaction.Rollback();
                } finally
                {
                    con.Close();
                }
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

        public List<Booking> GetCustomersBookings(int customerId)
        {
            List<Booking> bookings = new List<Booking>();
            string SQL_string = "SELECT * FROM Bookings WHERE customerId = @customerId";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter customerIdParam = new("@customerID", customerId);
                command.Parameters.Add(customerIdParam);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bookings.Add(GetBookingReader(reader));
                }
            }
            return bookings;
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

        public bool UpdateBooking(Booking newBooking)
        {
            int numberOfRowsModified = 0;

            string queryString = "UPDATE Bookings SET pickUpAddress=@PickUpAddress, returnAddress=@ReturnAddress, [status]=@Status, noOfBags=@NoOfBags WHERE id=@Id";

            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(queryString, con))
            {
                if (con != null)
                {
                    command.Parameters.AddWithValue("@PickUpAddress", newBooking.PickUpAddress);
                    command.Parameters.AddWithValue("@ReturnAddress", newBooking.ReturnAddress);
                    command.Parameters.AddWithValue("@Status", newBooking.BookingStatus);
                    command.Parameters.AddWithValue("@NoOfBags", newBooking.AmountOfBags);
                    command.Parameters.AddWithValue("@Id", newBooking.Id);

                    con.Open();
                    numberOfRowsModified = command.ExecuteNonQuery();
                }
            }
            return (numberOfRowsModified > 0);
        }

        public bool DeleteBooking(int id)
        {
            int numberOfRowsDeleted = 0;

            string SQL_string = "DELETE FROM Bookings WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand command = new(SQL_string, conn))
            {
                SqlParameter idParam = new("@id", id);
                command.Parameters.Add(idParam);

                if (conn != null)
                {
                    conn.Open();
                    numberOfRowsDeleted = command.ExecuteNonQuery();
                }
            }
            return (numberOfRowsDeleted > 0);
        }

        public List<string> GetAddressesByTimeslotId(int id)
        {
            List<string> addressList = new();
            string SQL_string1 = "SELECT pickUpAddress FROM Bookings WHERE pickUpTimeId = @PickUpTimeId";
            string SQL_string2 = "SELECT returnAddress FROM Bookings WHERE returnTimeId = @ReturnTimeId";
            using (SqlConnection con = new(_connectionString))
            {
                con.Open();
                SqlDataReader reader;
                using (SqlCommand command = new(SQL_string1, con))
                {
                    SqlParameter idParam = new("@PickUpTimeId", id);
                    command.Parameters.Add(idParam);

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        addressList.Add(reader.GetString(reader.GetOrdinal("pickUpAddress")));
                    }

                    reader.Close();
                }
                using (SqlCommand command = new(SQL_string2, con))
                {
                    SqlParameter idParam = new("@ReturnTimeId", id);
                    command.Parameters.Add(idParam);

                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        addressList.Add(reader.GetString(reader.GetOrdinal("returnAddress")));
                    }
                }
                con.Close();
            }
            return addressList;
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
