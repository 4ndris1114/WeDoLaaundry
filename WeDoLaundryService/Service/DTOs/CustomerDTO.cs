using Data.Model_layer;
using Microsoft.AspNetCore.Mvc;

namespace Service.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }    
        public string Phone { get; set; } 
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public CustomerType CustomerType { get; set; }

        public CustomerDTO()
        {
        }

        public CustomerDTO(int id, string firstName, string lastName, string phone, string email, string city, string address, CustomerType customerType)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            City = city;
            Address = address;
            CustomerType = customerType;
        }
    }
}
