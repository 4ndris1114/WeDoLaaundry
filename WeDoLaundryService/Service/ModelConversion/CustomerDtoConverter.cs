using Data.Model_layer;
using Service.DTOs;

namespace Service.ModelConversion
{
    public class CustomerDtoConverter : MovelDtoConversion<Customer, CustomerReadDTO>
    {

        public override List<CustomerReadDTO> ToDtoCollection(List<Customer> dbList)
        {
            List<CustomerReadDTO> customerDtoList = new();
            CustomerReadDTO tempDto;
            foreach (var customer in dbList)
            {
                if (customer != null)
                {
                    tempDto = ToDto(customer);
                    customerDtoList.Add(tempDto);
                }
            }
            return customerDtoList;
        }

        public override CustomerReadDTO ToDto(Customer model)
        {
            Customer customer = model;
            CustomerReadDTO? returnCustomerDto = null;

            if (customer != null)
            {
                returnCustomerDto = new CustomerReadDTO(customer.Id, customer.FirstName, customer.LastName, customer.Phone, customer.Email, customer.PostalCode, customer.City, customer.Address, (int)customer.CustomerType, customer.UserId);
            }

            return returnCustomerDto;
        }

        public override Customer ToModel(CustomerReadDTO dto)
        {
            CustomerReadDTO customerReadDto = dto;
            Customer? returnCustomer = null;

            if (customerReadDto != null)
            {
                returnCustomer = new Customer(customerReadDto.Id, customerReadDto.FirstName,
                    customerReadDto.LastName, customerReadDto.Phone, customerReadDto.Email, customerReadDto.PostalCode,
                    customerReadDto.City, customerReadDto.Address, customerReadDto.CustomerType, customerReadDto.UserId);
            }

            return returnCustomer;
        }
    }    
}
