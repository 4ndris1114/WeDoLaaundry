﻿using Model.Model_layer;

namespace Service.BusinessLogicLayer
{
    public interface ITimeslotDataControl
    {
        bool ModifyAvailability(int id, bool mode);
        TimeSlot Get(int id);
        bool Delete(int id);
        int Add(TimeSlot timeslot);
        List<TimeSlot> GetAll();
        TimeSlot? GetByDateAndSlot(DateTime date, string slot);
        List<TimeSlot> GetByDate(DateTime date);

    }
}