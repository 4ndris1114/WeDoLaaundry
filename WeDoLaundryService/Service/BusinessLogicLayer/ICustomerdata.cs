using Data.Model_layer;

namespace Service.BusinessLogicLayer
{
    public interface ICustomerdata
    {
        List<Customer> Get();
        Customer Get(int id);
    }
}
