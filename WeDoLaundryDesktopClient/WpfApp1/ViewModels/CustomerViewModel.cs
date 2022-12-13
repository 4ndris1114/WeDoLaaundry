using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.ViewModels
{
    public enum CustomerType
    {
        NO_SUBSCRIPTION = 0,
        NORMAL_SUBSCRIPTION = 1,
        PREMIUM_SUBSCRIPTION = 2
    }

    public class CustomerViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public CustomerType CustomerType { get; set; }
        public string UserId { get; set; }

        public CustomerViewModel(Customer customer)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Phone = customer.Phone;
            Email = customer.Email;
            PostalCode = customer.PostalCode;
            City = customer.City;
            Address = customer.Address;
            CustomerType = (CustomerType) customer.CustomerType;
            UserId = customer.UserId;
        }
    }
}
