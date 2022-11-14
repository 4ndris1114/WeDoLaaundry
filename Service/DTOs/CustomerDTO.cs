using Data.Model_layer;

namespace Service.DTOs
{
    public class CustomerDTO
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }    
        public string Phone { get; set; } 
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string customerType { get; set; }

        public CustomerDTO()
        {

        }

        public CustomerDTO(string firstName, string lastName, string phone, string email, string city, string address, CustomerType customerType)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            City = city;
            Address = address;
            this.customerType = customerType.ToString();
        }
    }
}
