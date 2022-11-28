using Data.Model_layer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Model.Model_layer;
using Service.BusinessLogicLayer;
using Service.DTOs;
using Service.ModelConversion;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeslotsController : ControllerBase
    {

        private readonly ITimeslotDataControl _timeslotDataControl;
        private readonly IConfiguration _configuration;
        private readonly TimeslotDtoConverter Convert;

        public TimeslotsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _timeslotDataControl = new TimeslotdataControl(_configuration);
            Convert = new();
        }

        [HttpGet]
        public ActionResult<List<TimeslotReadDTO>> Get()
        {

            ActionResult<List<TimeslotReadDTO>> returnList;

            List<TimeSlot>? foundTimeslots = _timeslotDataControl.GetAll();
            List<TimeslotReadDTO>? foundDtos = null;

            if (foundTimeslots != null)
            {
                foundDtos = Convert.ToDtoCollection(foundTimeslots);
            }
            //evaluate & return status code
            if (foundDtos != null)
            {
                if (foundDtos.Count > 0)
                {
                    returnList = Ok(foundDtos);
                }
                else
                {
                    returnList = new StatusCodeResult(204); //ok but no content
                }
            }
            else
            {
                returnList = new StatusCodeResult(500);
            }
            return returnList;
        }

        [HttpGet, Route("{id}")]
        public ActionResult<TimeslotReadDTO>? Get(int id)
        {
            ActionResult<TimeslotReadDTO> returnTimeslotDto;

            TimeSlot? foundTimeslot = _timeslotDataControl.Get(id);
            TimeslotReadDTO? foundTimeslotDto = Convert.ToDto(foundTimeslot);
            //evaluate & return status code
            if (foundTimeslotDto != null)
            {
                returnTimeslotDto = Ok(foundTimeslot);
            }
            else
            {
                returnTimeslotDto = new StatusCodeResult(500);
            }

            return returnTimeslotDto;
        }

        [HttpPut, Route("/decrease/{id}")]
        public ActionResult<bool>? Decrease(int id)
        {
            ActionResult<bool> retVal;

            bool wasUpdated;

            //TimeSlot? dbTimeslot = ModelConversion.TimeslotDtoConverter.ToTimeslot(timeslotDto);
            wasUpdated = _timeslotDataControl.DecreaseAvailability(id);
            //evaluate & return status code
            if (wasUpdated)
            {
                retVal = Ok(wasUpdated);
            }
            else
            {
                retVal = new StatusCodeResult(500);
            }

            return retVal;
        }

        [HttpDelete, Route("{id}")]
        public ActionResult Delete(int id)
        {
            ActionResult retVal;
            bool wasOk = _timeslotDataControl.Delete(id);

            if (wasOk)
            {
                retVal = Ok();
            }
            else
            {
                retVal = new StatusCodeResult(500);
            }
            return retVal;
        }

        [HttpPost]
        public ActionResult<int> Post(TimeslotReadDTO timeslotReadDto)
        {

            ActionResult<int> retVal;
            int insertedId = -1;

            if (timeslotReadDto != null)
            {
                TimeSlot? dbTimeSlot = Convert.ToModel(timeslotReadDto);
                insertedId = _timeslotDataControl.Add(dbTimeSlot);
            }
            if (insertedId > 0)
            {
                retVal = Ok(insertedId);
            }
            else
            {
                retVal = new StatusCodeResult(500);
            }

            return retVal;
        }
    }
}
