using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Security.Claims;
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
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            List<TimeSlot> timeSlotList = await _timeslotLogic.GetAll();
            List<SelectListItem> returnDayList = new List<SelectListItem>();
            List<SelectListItem> returnSlotList = new List<SelectListItem>();

            foreach (var item in timeSlotList)
            {
                returnDayList.Add(new SelectListItem() { 
                    Text = item.Date.ToString("ddd d MMM"),
                    Value = item.Date.ToString("ddd d MMM")
                });

                returnSlotList.Add(new SelectListItem()
                {
                    Text = item.Slot.ToString(),
                    Value = item.Slot.ToString()
                });
            }

            ViewBag.ListofDays = returnDayList;
            ViewBag.ListofSlots = returnSlotList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookingForm bookingForm)
            {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            string pickUpDate = bookingForm.PickUpDay.ToString("yyyy-MM-dd");
            string pickUpSlot = bookingForm.PickUpTimeSlot;
            string returnDate = bookingForm.ReturnDay.ToString("yyyy-MM-dd");
            string returnSlot = bookingForm.ReturnTimeSlot;

            var claimsId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;


            if (ModelState.IsValid)
            {
                int pickUpSlotObj = await _timeslotLogic.GetByDayAndSlot(pickUpDate, pickUpSlot);
                int returnSlotObj = await _timeslotLogic.GetByDayAndSlot(returnDate, returnSlot);
                Customer tempCustomer = await _customerLogic.GetCustomerByUserId(claimsId);
                Booking booking = new Booking(tempCustomer.Id, pickUpSlotObj, returnSlotObj, bookingForm.PickUpAddress, bookingForm.ReturnAddress, bookingForm.AmountOfBags);
                try
                {
                    bool wasOk = await _bookingLogic.InsertBooking(booking);
                    if (wasOk)
                    {
                        ViewBag.message = "Laundry registered";
                        return RedirectToAction("Success");
                    } else
                    {
                        ViewBag.message = "Bad request"; 
                    }
                }
                catch
                {
                    ViewBag.message = "Error while booking";
                    return View();
                }
            }
            return RedirectToAction("Index");
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

        [HttpGet]
        public ActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
