using Data.Model_layer;
using Service.DTOs;

namespace Service.ModelConversion
{
    public class DriverDtoConverter : MovelDtoConversion<Driver, DriverReadDTO>
    {

        public override List<DriverReadDTO> ToDtoCollection(List<Driver> dbList)
        {
            List<DriverReadDTO> driverDtoList = new();
            DriverReadDTO tempDto;
            foreach (var driver in dbList)
            {
                if (driver != null)
                {
                    tempDto = ToDto(driver);
                    driverDtoList.Add(tempDto);
                }
            }
            return driverDtoList;
        }

        public override DriverReadDTO ToDto(Driver model)
        {
            Driver driver = model;
            DriverReadDTO? returnDriverDto = null;

            if (driver != null)
            {
                returnDriverDto = new DriverReadDTO(driver.Id, driver.FirstName, driver.LastName);
            }

            return returnDriverDto;
        }

        public override Driver ToModel(DriverReadDTO dto)
        {
            DriverReadDTO driverReadDto = dto;
            Driver? returnDriver = null;

            if (driverReadDto != null)
            {
                returnDriver = new Driver(driverReadDto.Id, driverReadDto.FirstName, driverReadDto.LastName, driverReadDto.Phone, driverReadDto.PostalCode, driverReadDto.City, driverReadDto.Address, driverReadDto.Email, driverReadDto.Salary);
     
            }

            return returnDriver;
        }
    }
}
