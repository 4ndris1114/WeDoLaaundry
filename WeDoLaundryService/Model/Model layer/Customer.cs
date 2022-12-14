using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model_layer
{

    public enum CustomerType
    {
        NO_SUBSCRIPTION = 0, 
        NORMAL_SUBSCRIPTION = 1, 
        PREMIUM_SUBSCRIPTION = 2
    }

    public class Customer
    {
        public int Id { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public CustomerType CustomerType { get; set; }
        public string UserId { get; set; }

        public Customer(int id, string firstName, string lastName, string phone, string email, 
            int postalCode, string city, string address, CustomerType customerType, string userId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            PostalCode = postalCode;
            City = city;
            Address = address;
            CustomerType = customerType;
            UserId = userId;
        }

        public Customer(string firstName, string lastName, string phone, string email,
            int postalCode, string city, string address, CustomerType customerType, string userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            PostalCode = postalCode;
            City = city;
            Address = address;
            CustomerType = customerType;
            UserId = userId;
        }

        public Customer()
        {

        }

        public int GetId()
        {
            return Id;
        }
    }
}
