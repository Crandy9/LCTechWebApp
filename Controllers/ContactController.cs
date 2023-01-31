using Microsoft.AspNetCore.Mvc;

namespace LCWebApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
