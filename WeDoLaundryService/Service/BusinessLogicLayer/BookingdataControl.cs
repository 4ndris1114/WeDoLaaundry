﻿using Data.Database_layer;
using DataAccess.Database_layer;
using Model.Model_layer;

namespace Service.BusinessLogicLayer
{
    public class BookingdataControl : IBookingdataControl
    {
        private readonly IBookingDatabaseAccess _bookingDbAccess;

        public BookingdataControl(IConfiguration configuration)
        {
            _bookingDbAccess = new BookingDatabaseAccess(configuration);
        }

        public int Add(Booking booking)
        {
            int insertedId;
            try
            {
                insertedId = _bookingDbAccess.CreateBooking(booking);
            }
            catch
            {
                insertedId = -1;
            }
            return insertedId;
        }

        public bool Delete(int id)
        {
            bool wasDeleted;
            try
            {
                wasDeleted = _bookingDbAccess.DeleteBooking(id);
            }
            catch
            {
                wasDeleted = false;
            }
            return wasDeleted;
        }

        public List<Booking>? Get()
        {
            List<Booking>? foundBookings = null;
            try
            {
                foundBookings = _bookingDbAccess.GetAll();
            }
            catch
            {
                foundBookings = null;
            }
            return foundBookings;
        }

        public Booking GetById(int id)
        {
            Booking? foundBooking;
            try
            {
                foundBooking = _bookingDbAccess.Get(id);
            }
            catch
            {
                foundBooking = null;
            }
            return foundBooking;
        }

        public List<Booking> GetCustomersBookings(int customerId)
        {
            List<Booking> foundBookings;
            try
            {
                foundBookings = _bookingDbAccess.GetCustomersBookings(customerId);
            }
            catch 
            {
                foundBookings = null;
            }
            return foundBookings;
        }
    }
}
