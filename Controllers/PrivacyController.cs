using Microsoft.AspNetCore.Mvc;

namespace LCWebApp.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
