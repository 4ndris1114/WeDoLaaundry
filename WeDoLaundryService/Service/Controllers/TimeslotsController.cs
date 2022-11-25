using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    public class TimeslotsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
