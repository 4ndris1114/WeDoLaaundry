using Data.Model_layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.BusinessLogicLayer;
using Service.DTOs;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerdataControl _customerdataControl;
        private readonly IConfiguration _configuration;

        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
            _customerdataControl = new CustomerdataControl(configuration);
        }

        [HttpGet]
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
    }
}
