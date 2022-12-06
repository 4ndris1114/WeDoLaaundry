using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using WebAppIdentity.BusinessLogicLayer;
using WebAppIdentity.Data;
using WebAppIdentity.Models;

namespace WebAppIdentity.Controllers
{
    public class BookingsController : Controller
    {
        readonly BookingLogic _bookingLogic;
        readonly CustomerLogic _customerLogic;
        readonly TimeSlotLogic _timeslotLogic;

        public BookingsController()
        {
            _bookingLogic = new();
            _customerLogic = new();
            _timeslotLogic = new();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> History()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claimsId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            Customer tempCustomer = await _customerLogic.GetCustomerByUserId(claimsId);

            List<Booking> bookings = await _bookingLogic.GetCustomersBookings(tempCustomer.Id);
            ViewBag.Bookings = bookings;
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Success()
        {

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Create(string msg = "")
        {
            ViewBag.message = msg;
            List<TimeSlot> timeSlotList = await _timeslotLogic.GetAll();
            List<SelectListItem> returnList = new List<SelectListItem>();
            returnList.Add(new SelectListItem()
            {
                Text = "--- Select a date & time ---",
                Value = null
            });
            foreach (var item in timeSlotList)
            {
                returnList.Add(new SelectListItem()
                {
                    Text = item.Date.ToString("ddd d MMM") + " " + item.Slot.ToString(),
                    Value = item.Id.ToString()
                });

            }

            ViewBag.List = returnList;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookingForm bookingForm)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claimsId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (ModelState.IsValid)
            {
                Customer tempCustomer = await _customerLogic.GetCustomerByUserId(claimsId);
                if (await _timeslotLogic.validateOrder(bookingForm.PickUpDay, bookingForm.ReturnDay) &&
                    bookingForm.AmountOfBags <= _customerLogic.GetMaxAmountOfBags(tempCustomer))
                { // validates timeslots + amount of bags

                    Booking booking = new Booking(tempCustomer.Id, bookingForm.PickUpDay, bookingForm.ReturnDay, bookingForm.PickUpAddress, bookingForm.ReturnAddress, 0 /*Status: BOOKED*/, bookingForm.AmountOfBags);
                    try
                    {
                        bool wasOk = await _bookingLogic.InsertBooking(booking);
                        if (wasOk)
                        {
                            ViewBag.pickUpDate = await _timeslotLogic.GetById(bookingForm.PickUpDay);
                            ViewBag.returnDate = await _timeslotLogic.GetById(bookingForm.ReturnDay);
                            ViewBag.bookingInfo = booking;
                            return View("Success");
                        }
                        else
                        {
                            ViewBag.message = "Bad request";
                        }
                    }
                    catch (NullReferenceException)
                    {
                        ViewBag.message = "Error while booking";
                        return View();
                    }
                }
                else
                {
                    ViewBag.message = "Error while booking";
                    return RedirectToAction("Create", "Bookings");
                }
            }
            return RedirectToAction("Create", "Bookings");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool wasDeleted = await _bookingLogic.DeleteBooking(id);
                if (wasDeleted)
                {
                    return LocalRedirect("~/Identity/Account/Manage/CurrentOrder");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
