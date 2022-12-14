using Data.Model_layer;

namespace Service.BusinessLogicLayer
{
    public interface ICustomerdata
    {
        List<Customer>? Get();
        Customer GetByUserId(string userId);
        Customer GetById(int id);
        int Add(Customer name);
        bool Update(Customer customer);
        bool UpdateSub(int id, int subscription);
        bool Delete(int id);
    }
}
