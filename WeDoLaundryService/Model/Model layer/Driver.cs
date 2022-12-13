using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model_layer
{

    public class Driver
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
        public Driver(int id, string firstName, string lastName, string phone,int postalCode,string city,string address,string email,int salary)
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

        public Driver(string firstName, string lastName, string phone, int postalCode, string city, string address, string email, int salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            PostalCode = postalCode;
            City = city;
            Address = address;
            Email = email;
            Salary = salary;

        }

        public Driver()
        {

        }

        public int GetId()
        {
            return Id;
        }
    }
}
