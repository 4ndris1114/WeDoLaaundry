using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using WebAppIdentity.BusinessLogicLayer;
using WebAppIdentity.Data;
using WebAppIdentity.Models;

namespace WebAppIdentity.Controllers
{
    public class CustomersController : Controller
    {

        readonly CustomerLogic _customerLogic;

        public CustomersController()
        {
            _customerLogic = new();
        }

        // GET: CustomersController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        
        [HttpGet]
        // GET: CustomersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        [HttpGet]
        // GET: CustomersController/Delete/5
        public ActionResult Delete()
        {
            return View();
        }


        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            var claimsId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var claimsEmail = claimsIdentity.FindFirst(ClaimTypes.Email);


            if (ModelState.IsValid) {
                customer.Email = claimsEmail.Value;
                customer.UserId = claimsId.Value;
                try {
                    bool wasOk = await _customerLogic.InsertCustomer(customer);
                    if (wasOk){
                        ViewBag.message = "Customer registered";
                        return RedirectToAction("Index", "Home");
                    } else
                    {
                        ViewBag.message = "Bad request";
                    }
                }
                catch {
                    ViewBag.message = "Error while registering customer";
                    return View();
                }
            }
            return View();
        }


        // POST: CustomersController/Edit/5
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
        public async Task<ActionResult> Choose(int button)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claimsId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            bool wasUpdated;
            
            try
            {
                Customer tempCustomer = await _customerLogic.GetCustomerByUserId(claimsId);
                int customerId = tempCustomer.Id;
                wasUpdated = await _customerLogic.UpdateSubscription(customerId, button);
                if(wasUpdated)
                {
                    return RedirectToAction("Success", "Bookings");
                } 
            }
            catch 
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }


        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Customer customer)
        {
            bool wasDeleted;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claimsId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
         
            try
            {
                Customer tempCustomer = await _customerLogic.GetCustomerByUserId(claimsId);
                wasDeleted = await _customerLogic.DeleteCustomer(tempCustomer);
                if(wasDeleted)
                { 
                    return RedirectToAction("Index", "Home");   
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
