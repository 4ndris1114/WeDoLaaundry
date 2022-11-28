using Data.Model_layer;
using Model.Model_layer;
using Service.DTOs;

namespace Service.ModelConversion
{
    public class BookingDtoConverter : MovelDtoConversion<Booking, BookingReadDTO>
    {

        public override List<BookingReadDTO> ToDtoCollection(List<Booking> dbList)
        {
            List<BookingReadDTO> bookingDtoList = new();
            BookingReadDTO tempDto;
            foreach (var booking in dbList)
            {
                if (booking != null)
                {
                    tempDto = ToDto(booking);
                    bookingDtoList.Add(tempDto);
                }
            }
            return bookingDtoList;
        }

        public override BookingReadDTO ToDto(Booking model)
        {
            Booking booking = model;
            BookingReadDTO returnDto = null;

            if (booking != null)
            {
                returnDto = new BookingReadDTO(booking.Id, booking.Customer.GetId(), booking.DriverId, booking.PickUpTime.Id, booking.ReturnTime.Id, booking.PickUpAddress, booking.ReturnAddress, (int)booking.BookingStatus, booking.AmountOfBags, booking.InvoiceId);
            }
            return returnDto;
        }

        public override Booking ToModel(BookingReadDTO dto)
        {
            BookingReadDTO bookingReadDto = dto;
            Booking? returnBooking = null;

            if (bookingReadDto != null)
            {
                returnBooking = new Booking(bookingReadDto.Id, new(), bookingReadDto.DriverId, new(), new(), bookingReadDto.PickUpAddress, bookingReadDto.ReturnAddress, bookingReadDto.Status, bookingReadDto.AmountOfBags, bookingReadDto.InvoiceId);
            }
            return returnBooking;
        }
    }
}
