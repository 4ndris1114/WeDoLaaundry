using WebAppIdentity.Models;
using WebAppIdentity.Data.Migrations;
using WebAppIdentity.ServiceLayer;
using Microsoft.EntityFrameworkCore.Diagnostics;
using static WebAppIdentity.Models.Customer;

namespace WebAppIdentity.BusinessLogicLayer
{
    public class CustomerLogic : ICustomerLogic
    {

        private readonly ICustomerService _customerServiceAccess;

        public CustomerLogic()
        {
            _customerServiceAccess = new CustomerService();
        }

        public async Task<Customer> GetCustomerByUserId(string id)
        {
            Customer? returnCustomer;
            try
            {
                returnCustomer = await _customerServiceAccess.GetCustomerByUserId(id);
            }
            catch
            {
                returnCustomer = null;
            }
            return returnCustomer;
        }

        public async Task<bool> InsertCustomer(Customer customer)
        {
            bool wasInserted;
            try
            {
                wasInserted = await _customerServiceAccess.PostCustomer(customer);
            }
            catch
            {
                wasInserted = false;
            }

            return wasInserted;
        }

        //public async Task<Customer> CreateCustomer(Customer customer)
        //{
        //    Customer? returnCustomer;
        //    try
        //    {
        //        returnCustomer = await _customerServiceAccess.CreateCustomer(customer);
        //    }
        //    catch
        //    {
        //        returnCustomer = null;
        //    }
        //    return returnCustomer;
        //}

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            bool wasInserted;
            try
            {
                wasInserted = await _customerServiceAccess.UpdateCustomer(customer);
            }
            catch
            {
                wasInserted = false;
            }

            return wasInserted;
        }

        public async Task<bool> UpdateSubscription(Customer customer)
        {
            bool wasInserted;
            try
            {
                wasInserted = await _customerServiceAccess.UpdateSubscription(customer);
            }
            catch
            {
                wasInserted = false;
            }

            return wasInserted;
        }

        public async Task<bool> DeleteCustomer(Customer customer)
        {
            bool wasInserted;
            try
            {
                wasInserted = await _customerServiceAccess.DeleteCustomer(customer);
            }
            catch
            {
                wasInserted = false;
            }

            return wasInserted;
        }

        public int GetMaxAmountOfBags(Customer customer)
        {
            int maxAmount = 2;
            switch (customer.CustomerType)
            {
                case (Subscription) 0:
                    break;
                case (Subscription) 1:
                    maxAmount = 4;
                    break;
                case (Subscription) 2:
                    maxAmount = 6;
                    break;
                default:
                    break;
            }
            return maxAmount;
        }

    }
}

