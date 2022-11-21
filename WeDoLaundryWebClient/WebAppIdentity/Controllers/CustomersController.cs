using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        [Authorize]
        [HttpGet]
        // GET: CustomersController/Create
        public ActionResult Create(string userId)
        {
            return View();
        }

        [HttpPost]
        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid) {
                try {
                    bool wasOk = await _customerLogic.InsertCustomer(customer);
                    if (wasOk){
                        ViewBag.message = "Customer registered";
                        return RedirectToAction("Index");
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

        [HttpGet]
        // GET: CustomersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
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

        [HttpGet]
        // GET: CustomersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        // POST: CustomersController/Delete/5
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
