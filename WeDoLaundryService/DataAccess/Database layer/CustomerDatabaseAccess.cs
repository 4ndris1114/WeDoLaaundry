using Data.Database_layer;
using Data.Model_layer;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CustomerDatabaseAccess : ICustomerAccess
    {

        private readonly string _connectionString;

        public CustomerDatabaseAccess(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("WeDoLaundry");
        }

        public int CreateCustomer(Customer customer)
        {
            int insertedId = -1;

            string insertString = "INSERT INTO Customers(fname, lname, phone, postalCode, city, address, email, userId, userType) " +
                "OUTPUT INSERTED.ID VALUES(@FirstName, @LastName, @Phone, @PostalCode, @City, @Address, @Email, @UserId, @UserType)";

            using (SqlConnection con = new(_connectionString))
            using (SqlCommand cmd = new(insertString, con))
            {
                //Set parameters:
                SqlParameter fNameParam = new("@FirstName", customer.FirstName);
                cmd.Parameters.Add(fNameParam);
                SqlParameter lNameParam = new("@LastName", customer.LastName);
                cmd.Parameters.Add(lNameParam);
                SqlParameter phoneParam = new("@Phone", customer.Phone);
                cmd.Parameters.Add(phoneParam);
                SqlParameter postalCodeParam = new("@PostalCode", customer.PostalCode);
                cmd.Parameters.Add(postalCodeParam);
                SqlParameter cityParam = new("@City", customer.City);
                cmd.Parameters.Add(cityParam);
                SqlParameter addressParam = new("@Address", customer.Address);
                cmd.Parameters.Add(addressParam);
                SqlParameter emailParam = new("@Email", customer.Email);
                cmd.Parameters.Add(emailParam);
                SqlParameter userIdParam = new("@UserId", customer.UserId);
                cmd.Parameters.Add(userIdParam);
                SqlParameter userTypeParam = new("@UserType", customer.CustomerType);
                cmd.Parameters.Add(userTypeParam);
                //open connection
                con.Open();
                //execute insertion and read autogen. key (id)
                insertedId = (int)cmd.ExecuteScalar();

                con.Close();
            }
            return insertedId;
        }

        public List<Customer>? getAllCustomers()
        {
            List<Customer>? returnList = new();

            string SQL_string = "SELECT * FROM Customers";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                returnList = new();
                while (reader.Read())
                {
                    returnList.Add(GetCustomerReader(reader));
                }
                con.Close();
            }
            return returnList;
        }

        public Customer GetById(int id)
        {
            Customer customer = new();

            string SQL_string = "SELECT * FROM Customers WHERE id = @id";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter idParam = new("@id", id);
                command.Parameters.Add(idParam);


                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    customer = GetCustomerReader(reader);
                }
                con.Close();
            }

            return customer;
        }

        public Customer GetByUserId(string userId)
        {
            Customer customer = new();

            string SQL_string = "SELECT * FROM Customers WHERE userId = @UserId";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter idParam = new("@UserId", userId);
                command.Parameters.Add(idParam);


                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    customer = GetCustomerReader(reader);
                }
                con.Close();
            }

            return customer;
        }

        public bool UpdateCustomer(Customer customer)
        {
            int numberOfRowsModified = 0;

            string queryString = "UPDATE Customers SET fname=@FirstName, lname=@LastName, phone=@Phone, postalCode=@PostalCode, city=@City, address=@Address, email=@Email, userType=@CustomerType, userId=@UserId WHERE id=@Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(queryString, conn))
            {
                if(conn != null)
                {
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);
                    command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                    command.Parameters.AddWithValue("@City", customer.City);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@CustomerType", customer.CustomerType.ToString());
                    command.Parameters.AddWithValue("@Id", customer.Id);
                    command.Parameters.AddWithValue("@UserId", customer.UserId);

                    conn.Open();
                    numberOfRowsModified = command.ExecuteNonQuery();

                }
            }
            return (numberOfRowsModified > 0);
        }

        public bool UpdateSubscription(int id, int subscription)
        {
            int numberOfRowsModified = 0;
            string SQL_string = "UPDATE Customers SET userType = @UserType WHERE id = @Id";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter userTypeParam = new("@UserType", subscription);
                command.Parameters.Add(userTypeParam);
                SqlParameter idParam = new("@Id", id);
                command.Parameters.Add(idParam);

                con.Open();

                numberOfRowsModified = command.ExecuteNonQuery();
                if(numberOfRowsModified > 0)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }

        public bool DeleteCustomer(int id)
        {
            int numberOfRowsDeleted = 0;

            string SQL_string = "DELETE FROM Customers WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand command = new(SQL_string, conn))
            {
                SqlParameter idParam = new("@id", id);
                command.Parameters.Add(idParam);

                if(conn != null)
                {
                    conn.Open();
                    numberOfRowsDeleted = command.ExecuteNonQuery();
                }
            }
            return (numberOfRowsDeleted > 0);
        }

        private Customer GetCustomerReader(SqlDataReader reader)
        {
            Customer returnCustomer;
            int tempId = reader.GetInt32(reader.GetOrdinal("id"));
            string firstName = reader.GetString(reader.GetOrdinal("fname"));
            string lastName = reader.GetString(reader.GetOrdinal("lname"));
            string phone = reader.GetString(reader.GetOrdinal("phone"));
            string userId = "No account";
            if (!reader.IsDBNull(8))
            {
                userId = reader.GetString(reader.GetOrdinal("userId"));
            }
            string email = reader.GetString(reader.GetOrdinal("email"));
            int postalCode = reader.GetInt32(reader.GetOrdinal("postalCode"));
            string city = reader.GetString(reader.GetOrdinal("city"));
            string address = reader.GetString(reader.GetOrdinal("address"));
            CustomerType customerType = (CustomerType) Enum.Parse(typeof(CustomerType), reader.GetString(reader.GetOrdinal("userType")).ToUpper(), true);

            returnCustomer = new(tempId, firstName, lastName, phone, email, postalCode, city, address, customerType , userId);

            return returnCustomer;
        }

    }
}