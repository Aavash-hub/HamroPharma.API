using Microsoft.AspNetCore.Mvc;

namespace HamroPharma.API.Controllers
{
    public class VendorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
