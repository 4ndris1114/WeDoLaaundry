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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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