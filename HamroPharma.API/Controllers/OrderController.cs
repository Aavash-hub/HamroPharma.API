using Microsoft.AspNetCore.Mvc;

namespace HamroPharma.API.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
