using Data.Database_layer;
using Data.Model_layer;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DriverDatabaseAccess : IDriverAccess
    {

        private readonly string _connectionString;

        public DriverDatabaseAccess(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("WeDoLaundry");
        }

        public int CreateDriver(Driver driver)
        {
            int insertedId = -1;

            string insertString = "INSERT INTO Drivers(fname, lname, phone, postalCode, city, address, email, salary) " +
                "OUTPUT INSERTED.ID VALUES(@FirstName, @LastName, @Phone, @PostalCode, @City, @Address, @Email, @Salary)";

            using (SqlConnection con = new(_connectionString))
            using (SqlCommand cmd = new(insertString, con))
            {
                //Set parameters:
                SqlParameter fNameParam = new("@FirstName", driver.FirstName);
                cmd.Parameters.Add(fNameParam);
                SqlParameter lNameParam = new("@LastName", driver.LastName);
                cmd.Parameters.Add(lNameParam);
                SqlParameter phoneParam = new("@Phone", driver.Phone);
                cmd.Parameters.Add(phoneParam);
                SqlParameter postalCodeParam = new("@PostalCode", driver.PostalCode);
                cmd.Parameters.Add(postalCodeParam);
                SqlParameter cityParam = new("@City", driver.City);
                cmd.Parameters.Add(cityParam);
                SqlParameter addressParam = new("@Address", driver.FirstName);
                cmd.Parameters.Add(addressParam);
                SqlParameter emailParam = new("@Email", driver.Email);
                cmd.Parameters.Add(emailParam);
                SqlParameter salaryParam = new("@Salary", driver.Salary);
                cmd.Parameters.Add(salaryParam);
                //open connection
                con.Open();
                //execute insertion and read autogen. key (id)
                insertedId = (int)cmd.ExecuteScalar();

                con.Close();
            }
            return insertedId;
        }

        public List<Driver>? GetAllDrivers()
        {
            List<Driver>? returnList = new();

            string SQL_string = "SELECT * FROM Drivers";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                returnList = new();
                while (reader.Read())
                {
                    returnList.Add(GetDriverReader(reader));
                }
                con.Close();
            }
            return returnList;
        }

        public Driver GetById(int id)
        {
            Driver driver = new();

            string SQL_string = "SELECT * FROM Drivers WHERE id = @id";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter idParam = new("@id", id);
                command.Parameters.Add(idParam);


                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    driver = GetDriverReader(reader);
                }
                con.Close();
            }

            return driver;
        }

        public Driver GetByUserId(string userId)
        {
            Driver driver = new();

            string SQL_string = "SELECT * FROM Drivers WHERE userId = @UserId";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter idParam = new("@UserId", userId);
                command.Parameters.Add(idParam);


                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    driver = GetDriverReader(reader);
                }
                con.Close();
            }

            return driver;
        }

        public bool UpdateDriver(Driver driver)
        {
            int numberOfRowsModified = 0;

            string queryString = "UPDATE Drivers SET fname=@FirstName, lname=@LastName WHERE id=@Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(queryString, conn))
            {
                if (conn != null)
                {
                    command.Parameters.AddWithValue("@FirstName", driver.FirstName);
                    command.Parameters.AddWithValue("@LastName", driver.LastName);
                    command.Parameters.AddWithValue("@Id", driver.Id);

                    conn.Open();
                    numberOfRowsModified = command.ExecuteNonQuery();

                }
            }
            return (numberOfRowsModified > 0);
        }


        public bool DeleteDriver(int id)
        {
            int numberOfRowsDeleted = 0;

            string SQL_string = "DELETE FROM Drivers WHERE id = @id";

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

        private Driver GetDriverReader(SqlDataReader reader)
        {
            Driver returnDriver;
            int tempId = reader.GetInt32(reader.GetOrdinal("id"));
            string firstName = reader.GetString(reader.GetOrdinal("fname"));
            string lastName = reader.GetString(reader.GetOrdinal("lname"));
            string phone = reader.GetString(reader.GetOrdinal("phone"));
            int postalCode = reader.GetInt32(reader.GetOrdinal("postalCode"));
            string city = reader.GetString(reader.GetOrdinal("city"));
            string address = reader.GetString(reader.GetOrdinal("address"));
            string email = reader.GetString(reader.GetOrdinal("email"));
            int salary = reader.GetInt32(reader.GetOrdinal("salary"));

            returnDriver = new(tempId,firstName, lastName, phone, postalCode, city, address,email,salary);

            return returnDriver;
        }

    }
}