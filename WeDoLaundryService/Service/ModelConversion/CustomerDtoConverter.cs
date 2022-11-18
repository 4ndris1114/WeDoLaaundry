using Data.Model_layer;
using Service.DTOs;

namespace Service.ModelConversion
{
    public class CustomerDtoConverter
    {
        public static List<CustomerReadDTO> ToDtoCollection(List<Customer> customerList)
        {
            List<CustomerReadDTO> customerDtoList = new();
            CustomerReadDTO tempDto;
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
        
        public static CustomerReadDTO ToCustomerDto(Customer customer)
        {
            CustomerReadDTO returnCustomerDto = null;

            if (customer != null)
            {
                returnCustomerDto = new CustomerReadDTO(customer.Id, customer.FirstName, customer.LastName, customer.Phone, customer.Email, customer.PostalCode, customer.City, customer.Address, customer.CustomerType);
            }

            return returnCustomerDto;
        }

        public static Customer ToCustomerFromCustomerCreationDTO(CustomerCreationDTO customerCreationDto)
        {
            Customer returnCustomer = null;

            if (customerCreationDto != null)
            {
                returnCustomer = new Customer(customerCreationDto.Id, customerCreationDto.FirstName, 
                    customerCreationDto.LastName, customerCreationDto.Phone,customerCreationDto.Email, customerCreationDto.PostalCode,
                    customerCreationDto.City, customerCreationDto.Address, customerCreationDto.PasswordHash, customerCreationDto.CustomerType);
            }

            return returnCustomer;
        }

    }    
}
