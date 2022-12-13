using Data.Model_layer;

namespace Service.BusinessLogicLayer
{
    public interface IDriverdata
    {
        List<Driver>? Get();
        Driver GetByUserId(string userId);
        Driver GetById(int id);
        int Add(Driver name);
        bool Update(Driver driver);
        bool Delete(int id);
    }
}
