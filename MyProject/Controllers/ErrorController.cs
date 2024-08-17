using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
