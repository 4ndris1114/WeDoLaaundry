using Data.Model_layer;
using Microsoft.AspNetCore.Mvc;
using Model.Model_layer;
using Service.BusinessLogicLayer;
using Service.DTOs;
using Service.ModelConversion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingdataControl _bookingdataControl;
        private readonly IConfiguration _configuration;
        private readonly ICustomerdata _customerdataControl;
        private readonly ITimeslotDataControl _timeslotDataControl;
        private readonly BookingDtoConverter Convert;

        public BookingsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _bookingdataControl = new BookingdataControl(configuration);
            _customerdataControl = new CustomerdataControl(configuration);
            _timeslotDataControl = new TimeslotdataControl(configuration);
            Convert = new();
        }

        // GET: api/<BookingsController>
        [HttpGet]
        public ActionResult<List<BookingReadDTO>> Get()
        {
            ActionResult<List<BookingReadDTO>> returnList = null;

            List<Booking>? foundBookings = _bookingdataControl.Get();
            List<BookingReadDTO>? foundDtos = null;

            if (foundBookings != null)
            {
                foundDtos = Convert.ToDtoCollection(foundBookings);
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

        // GET api/<BookingsController>/customerBookings/5
        [HttpGet, Route("customerBookings/{customerId}")]
        public ActionResult<List<BookingReadDTO>> GetCustomersBookings(int customerId)
        {
            ActionResult<List<BookingReadDTO>> returnList = null;

            List<Booking> foundBookings = _bookingdataControl.GetCustomersBookings(customerId);
            List<BookingReadDTO> foundDtos = null;
            //evaluate & return status code
            if (foundBookings != null)
            {
                foundDtos = Convert.ToDtoCollection(foundBookings);
            }
            
            if(foundDtos != null)
            {
                if(foundDtos.Count > 0)
                {
                    returnList = Ok(foundDtos);
                } else
                {
                    returnList = new StatusCodeResult(204);
                }
            } else
            {
                returnList = new StatusCodeResult(500);
            }
            return returnList;
        }

        // GET api/<BookingsController>/5
        [HttpGet, Route("{id}")]
        public ActionResult<BookingReadDTO> Get(int id)
        {
            ActionResult<BookingReadDTO> returnBookingDto;

            Booking? foundBooking = _bookingdataControl.GetById(id);
            BookingReadDTO? foundDto = Convert.ToDto(foundBooking);
            //evaluate & return status code
            if (foundDto != null)
            {
                if (foundDto.Id > 0)
                {
                    returnBookingDto = Ok(foundDto);
                }
                else
                {
                    returnBookingDto = new StatusCodeResult(204);
                }
            }
            else
            {
                returnBookingDto = new StatusCodeResult(500);
            }

            return returnBookingDto;
        }

        // POST api/<BookingsController>
        [HttpPost]
        public ActionResult<int> Post(BookingReadDTO bookingReadDto)
        {
            ActionResult<int> retVal;
            int insertedId = -1;

            if (bookingReadDto != null)
            {
                Booking? dbBooking = Convert.ToModel(bookingReadDto);
                dbBooking.Customer = _customerdataControl.GetById(bookingReadDto.CustomerId);
                dbBooking.PickUpTime = _timeslotDataControl.Get(bookingReadDto.PickUpTimeId);
                dbBooking.ReturnTime = _timeslotDataControl.Get(bookingReadDto.ReturnTimeId);
                insertedId = _bookingdataControl.Add(dbBooking);
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

        [HttpDelete, Route("{id}")]
        public ActionResult Delete(int id)
        {
            ActionResult retVal;
            bool wasOk = _bookingdataControl.Delete(id);

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

        //// PUT api/<BookingsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<BookingsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
