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

        [HttpGet]
        public ActionResult<List<CustomerReadDTO>> Get(){

            ActionResult<List<CustomerReadDTO>> returnList;

            List<Customer> foundCustomers = _customerdataControl.Get();
            List<CustomerReadDTO> foundDtos = null;

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

        [HttpGet, Route("{id}")]
        public ActionResult<CustomerReadDTO> Get(int id) {
            ActionResult<CustomerReadDTO> returnCustomerDto;

            Customer? foundCustomer = _customerdataControl.Get(id);
            CustomerReadDTO? foundCustomerDto = ModelConversion.CustomerDtoConverter.ToCustomerDto(foundCustomer);
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

        [HttpPost]
        public ActionResult<int> Post(CustomerCreationDTO customerCreationDTO) {

            ActionResult<int> retVal;
            int insertedId = -1;
            
            if (customerCreationDTO != null)
            {
                Customer dbCustomer = ModelConversion.CustomerDtoConverter.ToCustomerFromCustomerCreationDTO(customerCreationDTO);
                insertedId = _customerdataControl.Add(dbCustomer);
            }
            if (insertedId > 0)
            {
                retVal = Ok(insertedId);
            } else {
                retVal = new StatusCodeResult(500);
            }

            return retVal;
        }
    }
}
