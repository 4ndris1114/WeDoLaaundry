using Data.Database_layer;
using Data.Model_layer;
using DataAccess;
using Xunit.Abstractions;

namespace CustomerDataTest
{
    public class CustomerDataTest
    {

        private readonly ITestOutputHelper extraOutput;
        private readonly ICustomerAccess _customerAccess;

        private readonly string _connectionString = "Server=hildur.ucn.dk,1433;Database=CSC-CSD-S211_10407531;User Id = CSC-CSD-S211_10407531; Password=Password1!";

        public CustomerDataTest(ITestOutputHelper extraOutput)
        {
            this.extraOutput = extraOutput;
            _customerAccess = new CustomerDatabaseAccess(_connectionString);
        }

        [Fact]
        public void testGetAllCustomer()
        {
            //Arrange

            //Act
            List<Customer> foundCustomers = _customerAccess.getAllCustomers();
            extraOutput.WriteLine("Found customers: "+foundCustomers.Count);

            //Assert
            Assert.True(foundCustomers.Count == 2);
        }
    }
}