using Data.Model_layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.BusinessLogicLayer;
using Service.DTOs;
using Service.ModelConversion;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerdata _customerdataControl;
        private readonly IConfiguration _configuration;
        private readonly CustomerDtoConverter Convert;

        public CustomersController(IConfiguration configuration)
        {
            _configuration = configuration;
            _customerdataControl = new CustomerdataControl(configuration);
            Convert = new();
        }

        [HttpGet]
        public ActionResult<List<CustomerReadDTO>> Get(){

            ActionResult<List<CustomerReadDTO>> returnList;

            List<Customer>? foundCustomers = _customerdataControl.Get();
            List<CustomerReadDTO>? foundDtos = null;

            if (foundCustomers != null)
            {
                foundDtos = Convert.ToDtoCollection(foundCustomers);
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
        public ActionResult<CustomerReadDTO>? Get(int id) {
            ActionResult<CustomerReadDTO> returnCustomerDto;

            Customer? foundCustomer = _customerdataControl.GetById(id);
            CustomerReadDTO? foundCustomerDto = Convert.ToDto(foundCustomer);
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

        [HttpGet, Route("account/{userId}")]
        public ActionResult<CustomerReadDTO>? GetByUserId(string userId)
        {
            ActionResult<CustomerReadDTO> returnCustomerDto;

            Customer? foundCustomer = _customerdataControl.GetByUserId(userId);
            CustomerReadDTO? foundCustomerDto = Convert.ToDto(foundCustomer);
            //evaluate & return status code
            if (foundCustomerDto != null)
            {
                if (foundCustomer.Id > 0)
                {
                    returnCustomerDto = Ok(foundCustomerDto);
                }
                else
                {
                    returnCustomerDto = new StatusCodeResult(204);
                }
            }
            else
            {
                returnCustomerDto = new StatusCodeResult(500);
            }

            return returnCustomerDto;
        }

        [HttpPost]
        public ActionResult<int> Post(CustomerReadDTO customerReadDto) {

            ActionResult<int> retVal;
            int insertedId = -1;
            
            if (customerReadDto != null)
            {
                Customer? dbCustomer = Convert.ToModel(customerReadDto);
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

        [HttpPut, Route("{id}")]
        public ActionResult Update(CustomerReadDTO customerDTO) {
            ActionResult retVal;
            Customer? inCustomer = Convert.ToModel(customerDTO);
            bool wasOk = _customerdataControl.Update(inCustomer);
            if (wasOk)
            {
                retVal = Ok();
            } else
            {
                retVal = new StatusCodeResult(500);
            }
            return retVal;
        }

        [HttpDelete, Route("{id}")]
        public ActionResult Delete(int id) {
            ActionResult retVal;
            bool wasOk = _customerdataControl.Delete(id);

            if(wasOk)
            {
                retVal = Ok();
            } else
            {
                retVal= new StatusCodeResult(500);
            }
            return retVal;
        }
    }
}
