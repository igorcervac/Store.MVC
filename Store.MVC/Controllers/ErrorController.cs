using Microsoft.AspNetCore.Mvc;

namespace Store.MVC.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("404")]
        public IActionResult Erro404()
        {
            return View("404");
        }

        [Route("500")]
        public IActionResult Error500() 
        {
            return View("500");
        }
    }
}
