using Data.Model_layer;
using Microsoft.AspNetCore.Mvc;
using Model.Model_layer;
using Service.BusinessLogicLayer;
using Service.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingdataControl _bookingdataControl;
        private readonly IConfiguration _configuration;
        private ICustomerdata _customerdataControl;

        public BookingsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _bookingdataControl = new BookingdataControl(configuration);
            _customerdataControl = new CustomerdataControl(configuration);
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
                foundDtos = ModelConversion.BookingDtoConverter.ToDtoCollection(foundBookings);
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

        // GET api/<BookingsController>/5
        [HttpGet, Route("{id}")]
        public ActionResult<BookingReadDTO> Get(int id)
        {
            ActionResult<BookingReadDTO> returnBookingDto;

            Booking? foundBooking = _bookingdataControl.GetById(id);
            BookingReadDTO? foundDto = ModelConversion.BookingDtoConverter.ToBookingDto(foundBooking);
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
                Booking? dbBooking = ModelConversion.BookingDtoConverter.ToBooking(bookingReadDto);
                dbBooking.Customer = _customerdataControl.GetById(bookingReadDto.CustomerId);
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
