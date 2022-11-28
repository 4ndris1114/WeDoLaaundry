using Data.Model_layer;
using Model.Model_layer;
using Service.DTOs;

namespace Service.ModelConversion
{
    public class TimeslotDtoConverter : MovelDtoConversion<TimeSlot, TimeslotReadDTO>
    {

        public override List<TimeslotReadDTO> ToDtoCollection(List<TimeSlot> dbList)
        {
            List<TimeslotReadDTO> timeslotDtoList = new();
            TimeslotReadDTO tempDto;
            foreach (var timeslot in dbList)
            {
                if (timeslot != null)
                {
                    tempDto = ToDto(timeslot);
                    timeslotDtoList.Add(tempDto);
                }
            }
            return timeslotDtoList;
        }

        public override TimeslotReadDTO ToDto(TimeSlot model)
        {
            TimeSlot timeslot = model;
            TimeslotReadDTO? returnTimeslotDto = null;

            if (timeslot != null)
            {
                returnTimeslotDto = new TimeslotReadDTO(timeslot.Id, timeslot.Date, timeslot.Slot, timeslot.Availability);
            }

            return returnTimeslotDto;
        }

        public override TimeSlot ToModel(TimeslotReadDTO dto)
        {
            TimeslotReadDTO timeslotDto = dto;
            TimeSlot? returnTimeslot = null;

            if (timeslotDto != null)
            {
                returnTimeslot = new TimeSlot(timeslotDto.Id, timeslotDto.Date, timeslotDto.Slot, timeslotDto.Availability);
            }

            return returnTimeslot;
        }
    }    
}
