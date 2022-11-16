using Data.Model_layer;

namespace Service.DTOs
{
    public class CustomerCreationDTO : CustomerDTO
    {

        public char[] Password { get;  set; }

        public CustomerCreationDTO(int id, string firstName, string lastName, string phone, 
            string email, string city, string address, CustomerType customerType, char[] password) : base(id, firstName, lastName, phone, email, city, address, customerType)
        {
            Password = password;
        }
    }
}
