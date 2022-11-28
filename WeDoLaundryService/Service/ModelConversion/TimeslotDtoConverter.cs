using Data.Model_layer;
using Model.Model_layer;
using Service.DTOs;

namespace Service.ModelConversion
{
    public class TimeslotDtoConverter
    {
        public static List<TimeslotReadDTO> ToDtoCollection(List<TimeSlot> timeslotList)
        {
            List<TimeslotReadDTO> timeslotDtoList = new();
            TimeslotReadDTO tempDto;
            foreach (var timeslot in timeslotList)
            {
                if (timeslot != null)
                {
                    tempDto = ToTimeslotDto(timeslot);
                    timeslotDtoList.Add(tempDto);
                }
            }
            return timeslotDtoList;
        }
        
        public static TimeslotReadDTO? ToTimeslotDto(TimeSlot timeslot)
        {
            TimeslotReadDTO? returnTimeslotDto = null;

            if (timeslot != null)
            {
                returnTimeslotDto = new TimeslotReadDTO(timeslot.Id, timeslot.Date, timeslot.Slot, timeslot.Availability);
            }

            return returnTimeslotDto;
        }

        public static TimeSlot? ToTimeslot(TimeslotReadDTO timeslotDto)
        {
            TimeSlot? returnTimeslot = null;

            if (timeslotDto != null)
            {
                returnTimeslot = new TimeSlot(timeslotDto.Id, timeslotDto.Date, timeslotDto.Slot, timeslotDto.Availability);
            }

            return returnTimeslot;
        }

    }    
}
