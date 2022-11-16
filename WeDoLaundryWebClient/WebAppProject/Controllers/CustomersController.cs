using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class CustomersController : Controller
    {

        

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

        [HttpGet]
        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                
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
