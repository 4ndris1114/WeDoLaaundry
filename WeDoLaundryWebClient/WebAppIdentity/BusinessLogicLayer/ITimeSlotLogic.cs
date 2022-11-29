﻿using WebAppIdentity.Models;

namespace WebAppIdentity.BusinessLogicLayer
{
    public interface ITimeSlotLogic
    {
        Task<List<TimeSlot>> GetAll();
    }
}