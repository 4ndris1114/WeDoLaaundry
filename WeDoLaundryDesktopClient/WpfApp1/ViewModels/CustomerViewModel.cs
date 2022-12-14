using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; NotifyPropertyChanged("Availability"); }
        }
        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; NotifyPropertyChanged("Availability"); }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; NotifyPropertyChanged("Availability"); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; NotifyPropertyChanged("Availability"); }
        }

        private int postalCode;

        public int PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; NotifyPropertyChanged("Availability"); }
        }

        private string city;

        public string City
        {
            get { return city; }
            set { city = value; NotifyPropertyChanged("Availability"); }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; NotifyPropertyChanged("Availability"); }
        }

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

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
