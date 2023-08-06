using Microsoft.AspNetCore.Mvc;

namespace Store.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }
    }
}
