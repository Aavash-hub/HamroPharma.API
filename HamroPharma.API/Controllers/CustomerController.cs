using Microsoft.AspNetCore.Mvc;

namespace HamroPharma.API.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
