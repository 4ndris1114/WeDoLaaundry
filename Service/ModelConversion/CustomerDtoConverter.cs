using Data.Model_layer;
using Service.DTOs;

namespace Service.ModelConversion
{
    public class CustomerDtoConverter
    {
        public static List<CustomerDTO> ToDtoCollection(List<Customer> customerList)
        {
            List<CustomerDTO> customerDtoList = new();
            CustomerDTO tempDto;
            foreach (var customer in customerList)
            {
                if (customer != null)
                {
                    tempDto = ToCustomerDto(customer);
                    customerDtoList.Add(tempDto);
                }
            }
            return customerDtoList;
        }
        
        public static CustomerDTO ToCustomerDto(Customer customer)
        {
            CustomerDTO returnCustomerDto = null;

            if (customer != null)
            {
                returnCustomerDto = new CustomerDTO(customer.FirstName, customer.LastName, customer.Phone, customer.Email, customer.City, customer.Address, customer.CustomerType);
            }

            return returnCustomerDto;
        }
    }    
}
