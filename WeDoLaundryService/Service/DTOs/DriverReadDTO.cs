using Data.Model_layer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Service.DTOs
{
    public class DriverReadDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }

        public DriverReadDTO()
        {
        }

        public DriverReadDTO(int id, string firstName, string lastName, string phone, int postalCode, string city, string address, string email, int salary)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            PostalCode = postalCode;
            City = city;
            Address = address;
            Email = email;
            Salary = salary;
        }
    }
}
