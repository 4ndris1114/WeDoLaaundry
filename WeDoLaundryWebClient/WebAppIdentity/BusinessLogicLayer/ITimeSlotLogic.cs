using WebAppIdentity.Models;

namespace WebAppIdentity.BusinessLogicLayer
{
    public interface ITimeSlotLogic
    {
        Task<List<TimeSlot>> GetAll();
        Task<TimeSlot> GetById(int id);
        Task<int> GetByDayAndSlot(string date, string slot);

        Task<bool> validateOrder(int id1, int id2);
    }
}
