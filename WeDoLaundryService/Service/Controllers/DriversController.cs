using Data.Model_layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.BusinessLogicLayer;
using Service.DTOs;
using Service.ModelConversion;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {

        private readonly IDriverdata _driverdataControl;
        private readonly IConfiguration _configuration;
        private readonly DriverDtoConverter Convert;

        public DriversController(IConfiguration configuration)
        {
            _configuration = configuration;
            _driverdataControl = new DriverdataControl(configuration);
            Convert = new();
        }

        [HttpGet]
        public ActionResult<List<CustomerReadDTO>> Get()
        {

            ActionResult<List<CustomerReadDTO>> returnList;

            List<Driver>? foundDrivers = _driverdataControl.Get();
            List<DriverReadDTO>? foundDtos = null;

            if (foundDrivers != null)
            {
                foundDtos = Convert.ToDtoCollection(foundDrivers);
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
        public ActionResult<DriverReadDTO>? Get(int id)
        {
            ActionResult<DriverReadDTO> returnDriverDto;

            Driver? foundDriver = _driverdataControl.GetById(id);
            DriverReadDTO? foundDriverDto = Convert.ToDto(foundDriver);
            //evaluate & return status code
            if (foundDriverDto != null)
            {
                if (foundDriver.Id > 0)
                {
                    returnDriverDto = Ok(foundDriverDto);
                }
                else
                {
                    returnDriverDto = new StatusCodeResult(204);
                }
            }
            else
            {
                returnDriverDto = new StatusCodeResult(500);
            }

            return returnDriverDto;
        }

        [HttpGet, Route("account/{userId}")]
        public ActionResult<DriverReadDTO>? GetByUserId(string userId)
        {
            ActionResult<DriverReadDTO> returnDriverDto;

            Driver? foundDriver = _driverdataControl.GetByUserId(userId);
            DriverReadDTO? foundDriverDto = Convert.ToDto(foundDriver);
            //evaluate & return status code
            if (foundDriverDto != null)
            {
                if (foundDriver.Id > 0)
                {
                    returnDriverDto = Ok(foundDriverDto);
                }
                else
                {
                    returnDriverDto = new StatusCodeResult(204);
                }
            }
            else
            {
                returnDriverDto = new StatusCodeResult(500);
            }

            return returnDriverDto;
        }

        [HttpPost]
        public ActionResult<int> Post(DriverReadDTO driverReadDto)
        {

            ActionResult<int> retVal;
            int insertedId = -1;

            if (driverReadDto != null)
            {
                Driver? dbCustomer = Convert.ToModel(driverReadDto);
                insertedId = _driverdataControl.Add(dbCustomer);
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

        [HttpPut, Route("{id}")]
        public ActionResult Update(DriverReadDTO driverDTO)
        {
            ActionResult retVal;
            Driver? inDriver = Convert.ToModel(driverDTO);
            bool wasOk = _driverdataControl.Update(inDriver);
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

        [HttpDelete, Route("{id}")]
        public ActionResult Delete(int id)
        {
            ActionResult retVal;
            bool wasOk = _driverdataControl.Delete(id);

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
    }
}
