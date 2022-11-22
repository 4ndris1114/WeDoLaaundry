using Data.Database_layer;
using Data.Model_layer;
using DataAccess;
using System.Runtime.InteropServices;
using Xunit.Abstractions;

namespace CustomerDataTest
{
    public class CustomerDataTest
    {

        private readonly ITestOutputHelper extraOutput;
        private readonly ICustomerAccess _customerAccess;

        private readonly string _connectionString = "Server=hildur.ucn.dk,1433;Database=CSC-CSD-S211_10407554;User Id = CSC-CSD-S211_10407554; Password=Password1!";

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
            Assert.True(foundCustomers != null);
        }

        [Fact]
        public void testGetCustomerById()
        {
            //Arrange

            //Act
            Customer foundCustomer = _customerAccess.GetCustomerById(1000);
            extraOutput.WriteLine("Found customer: " + foundCustomer.Id);

            //Assert
            Assert.True(foundCustomer != null);
        }

        [Fact]
        public void testCreateCustomer()
        {
            //Arrange
            int insertId = -1;
            Customer newCustomer = new Customer("Test", "Test", "12345678", "test@test.test", 1234, "TestCity", 
                "Test street test", CustomerType.NO_SUBSCRIPTION, "14fcb003-819d-4fd1-b839-1394573eb427");

            //Act
            insertId = _customerAccess.CreateCustomer(newCustomer);
            extraOutput.WriteLine("Generated key (id): " + insertId);

            //Assert
            Assert.True(insertId != -1);


        }
    }
}