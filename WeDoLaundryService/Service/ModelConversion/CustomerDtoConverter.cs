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
        
        public static CustomerReadDTO? ToCustomerDto(Customer customer)
        {
            CustomerReadDTO? returnCustomerDto = null;

            if (customer != null)
            {
                returnCustomerDto = new CustomerReadDTO(customer.Id, customer.FirstName, customer.LastName, customer.Phone, customer.Email, customer.PostalCode, customer.City, customer.Address, customer.CustomerType);
            }

            return returnCustomerDto;
        }

        public static Customer? ToCustomer(CustomerReadDTO customerReadDto)
        {
            Customer? returnCustomer = null;

            if (customerReadDto != null)
            {
                returnCustomer = new Customer(customerReadDto.Id, customerReadDto.FirstName,
                    customerReadDto.LastName, customerReadDto.Phone, customerReadDto.Email, customerReadDto.PostalCode,
                    customerReadDto.City, customerReadDto.Address, customerReadDto.CustomerType);
            }

            return returnCustomer;
        }

    }    
}
