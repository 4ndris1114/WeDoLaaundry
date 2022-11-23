using Model.Model_layer;
using Service.DTOs;

namespace Service.ModelConversion
{
    public class BookingDtoConverter
    {
        public static List<BookingReadDTO> ToDtoCollection(List<Booking> bookingList)
        {
            List<BookingReadDTO> bookingDtoList = new();
            BookingReadDTO tempDto;
            foreach (var booking in bookingList)
            {
                if(booking != null)
                {
                    tempDto = ToBookingDto(booking);
                    bookingDtoList.Add(tempDto);
                }
            }
            return bookingDtoList;
        }

        public static BookingReadDTO? ToBookingDto(Booking booking)
        {
            BookingReadDTO returnDto = null;

            if(booking != null)
            {
                returnDto = new BookingReadDTO(booking.Id, booking.Customer, booking.DriverId, booking.PickUpTime, booking.ReturnTime, booking.PickUpAddress, booking.ReturnAddress, booking.BookingStatus, booking.AmountOfBags, booking.InvoiceId);
            }
            return returnDto;
        }

        public static Booking? ToBooking(BookingReadDTO bookingReadDto)
        {
            Booking? returnBooking = null;

            if(bookingReadDto != null)
            {
                returnBooking = new Booking(bookingReadDto.Id, bookingReadDto.Customer, bookingReadDto.DriverId, bookingReadDto.PickUpTime, bookingReadDto.ReturnTime, bookingReadDto.PickUpAddress, bookingReadDto.ReturnAddress, bookingReadDto.BookingStatus, bookingReadDto.AmountOfBags, bookingReadDto.InvoiceId);
            }
            return returnBooking;
        }
    }
}
