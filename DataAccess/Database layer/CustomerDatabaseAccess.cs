using Data.Database_layer;
using Data.Model_layer;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CustomerDatabaseAccess : ICustomerAccess
    {

        readonly string _connectionString;

        public CustomerDatabaseAccess(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("WeDoLaundry");
        }

        public CustomerDatabaseAccess(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public int CreateCustomer(Customer customer)
        {
            int insertedId = -1;

            string insertString = "INSERT INTO Customer(fname, lname, phone, postalCode, city, address, email, password_hash, userType) " +
                "OUTPUT INSERTED.ID VALUES(@FirstName, @LastName, @Phone, @PostalCode, @City, @Address, @Email, @PasswordHash, @UserType)";

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
                SqlParameter passwordHashParam = new("@PasswordHash", customer.Password);
                cmd.Parameters.Add(passwordHashParam);
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

            string SQL_string = "SELECT * FROM Customer";
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

        public Customer GetCustomerById(int id)
        {
            Customer customer = new();

            string SQL_string = "SELECT * FROM Customer WHERE [id] = @id";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                con.Open();
                SqlParameter idParam = new("@id", customer.Id);
                command.Parameters.Add(idParam);
                SqlDataReader reader = command.ExecuteReader();
                customer = GetCustomerReader(reader);
                con.Close();
            }

            return customer;
        }


        private Customer GetCustomerReader(SqlDataReader reader)
        {
            Customer returnCustomer;
            int tempId = reader.GetInt32(reader.GetOrdinal("id"));
            string firstName = reader.GetString(reader.GetOrdinal("fname"));
            string lastName = reader.GetString(reader.GetOrdinal("lname"));
            string phone = reader.GetString(reader.GetOrdinal("phone"));
            string email = reader.GetString(reader.GetOrdinal("email"));
            int postalCode = reader.GetInt32(reader.GetOrdinal("postalCode"));
            string city = reader.GetString(reader.GetOrdinal("city"));
            string address = reader.GetString(reader.GetOrdinal("address"));
            char[] password = reader.GetString(reader.GetOrdinal("password_hash")).ToCharArray();
            CustomerType customerType = (CustomerType) Enum.Parse(typeof(CustomerType), reader.GetString(reader.GetOrdinal("userType")).ToUpper(), true);

            returnCustomer = new(tempId, firstName, lastName, phone, email, postalCode, city, address, password, customerType);

            return returnCustomer;
        }

    }
}