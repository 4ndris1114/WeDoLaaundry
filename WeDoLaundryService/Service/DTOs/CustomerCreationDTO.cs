using Data.Model_layer;

namespace Service.DTOs
{
    public class CustomerCreationDTO : CustomerReadDTO
    {

        public string PasswordHash { get;  set; }

        public CustomerCreationDTO(int id, string firstName, string lastName, string phone, 
            string email, int postalCode, string city, string address, CustomerType customerType, string password) : base(id, firstName, lastName, phone, email, postalCode, city, address, customerType)
        {
            PasswordHash = password;
        }

        public CustomerCreationDTO()
        {

        }
    }
}
