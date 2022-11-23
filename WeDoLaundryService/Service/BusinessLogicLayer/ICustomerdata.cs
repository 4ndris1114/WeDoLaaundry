﻿using Data.Model_layer;

namespace Service.BusinessLogicLayer
{
    public interface ICustomerdata
    {
        List<Customer>? Get();
        Customer GetByUserId(string userId);
        Customer Get(int id);
        bool Update(Customer customer);
        bool Delete(int id);
    }
}
