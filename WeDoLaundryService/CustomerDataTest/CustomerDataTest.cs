using Data.Database_layer;
using Data.Model_layer;
using DataAccess;
using System.Runtime.InteropServices;
using Xunit.Abstractions;

namespace CustomerDataTest
{
    public class CustomerDataTest : IDisposable
    {

        private readonly ITestOutputHelper extraOutput;
        private readonly ICustomerAccess _customerAccess;
        private readonly MemoryStream _stream;
        private readonly string _connectionString = "Server=hildur.ucn.dk,1433;Database=CSC-CSD-S211_10407554;User Id = CSC-CSD-S211_10407554; Password=Password1!";
        private readonly int insertId;
        private readonly Customer newCustomer;

        public CustomerDataTest(ITestOutputHelper extraOutput)
        {
            this.extraOutput = extraOutput;
            _customerAccess = new CustomerDatabaseAccess(_connectionString);
            _stream = new MemoryStream();
            newCustomer = new Customer("Test", "Test", "12345678", "test@test.test", 1234, "TestCity",
                "Test street test", CustomerType.NO_SUBSCRIPTION, "05d8be71-4f1f-4fd7-a2d7-95d7ce64c632");
            insertId = _customerAccess.CreateCustomer(newCustomer);
        }

        public void Dispose()
        {
            _stream.Dispose();
            _customerAccess.DeleteCustomer(insertId);

        }

        [Fact]
        public void testGetAllCustomer()
        {
            //Arrange

            //Act
            List<Customer> foundCustomers = _customerAccess.getAllCustomers();
            extraOutput.WriteLine("Found customers: " + foundCustomers.Count);

            //Assert
            Assert.True(foundCustomers.Count > 0);
        }

        [Fact]
        public void testGetCustomerById()
        {
            //Arrange

            //Act
            Customer foundCustomer = _customerAccess.GetById(1006);
            String customerPhone = foundCustomer.Phone;
            extraOutput.WriteLine("Found customer: " + foundCustomer.Id);

            //Assert
            Assert.True(foundCustomer.Phone != null);
        }

        [Fact]
        public void testCreateCustomer()
        {
            //Arrange

            //Act
            int insertId = _customerAccess.CreateCustomer(newCustomer);
            extraOutput.WriteLine("Generated key (id): " + insertId);
            _customerAccess.DeleteCustomer(insertId);

            //Assert
            Assert.True(insertId != -1);
        }

        [Fact]
        public void testUpdateCustomer()
        {
            //Arrange
            string originalValue = newCustomer.Phone;
            String newPhone = "234567887"; // new value to be checked
            Customer updatedCustomer = newCustomer;
            newCustomer.Phone = newPhone;

            //Act
            bool updateReturnedTrue = _customerAccess.UpdateCustomer(updatedCustomer);
            Customer retrievedCustomer = _customerAccess.GetById(insertId);
            String updatedValue = retrievedCustomer.Phone;
            extraOutput.WriteLine("Original value: " + originalValue);
            extraOutput.WriteLine("Retrieved value: " + newPhone);

            //Assert
            Assert.True(updateReturnedTrue);
            Assert.True(updatedValue == newPhone);
        }
    }
}