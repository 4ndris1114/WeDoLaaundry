using Data.Model_layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.BusinessLogicLayer;
using Service.DTOs;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly CustomerdataControl _customerdataControl;
        private readonly IConfiguration _configuration;

        public CustomersController(IConfiguration configuration)
        {
            _configuration = configuration;
            _customerdataControl = new CustomerdataControl(configuration);
        }

        [HttpGet, Route("customers")]
        public ActionResult<List<CustomerDTO>> Get(){

            ActionResult<List<CustomerDTO>> returnList;

            List<Customer> foundCustomers = _customerdataControl.Get();
            List<CustomerDTO> foundDtos = null;

            if (foundCustomers != null)
            {
                foundDtos = ModelConversion.CustomerDtoConverter.ToDtoCollection(foundCustomers);
            }
            //evaluate & return status code
            if (foundDtos != null)
            {
                if (foundDtos.Count > 0)
                {
                    returnList = Ok(foundDtos);
                } else {
                    returnList = new StatusCodeResult(204); //ok but no content
                }
            } else {
                returnList = new StatusCodeResult(500);
            }
            return returnList;
        }

        [HttpGet, Route("customers/{id}")]
        public ActionResult<CustomerDTO> Get(int id) {
            ActionResult<CustomerDTO> returnCustomerDto;

            Customer? foundCustomer = _customerdataControl.Get(id);
            CustomerDTO? foundCustomerDto = ModelConversion.CustomerDtoConverter.ToCustomerDto(foundCustomer);
            //evaluate & return status code
            if (foundCustomerDto != null)
            {
                if (foundCustomer.Id > 0)
                {
                    returnCustomerDto = Ok(foundCustomerDto);
                } else
                {
                    returnCustomerDto = new StatusCodeResult(204);
                }
            } else {
                returnCustomerDto = new StatusCodeResult(500);
            }

            return returnCustomerDto;
        }

        //[HttpPost]
        //public ActionResult<CustomerDTO> Post(CustomerDTO customerDTO) {
        //    ActionResult<int> insertedId;
        //}
    }
}
