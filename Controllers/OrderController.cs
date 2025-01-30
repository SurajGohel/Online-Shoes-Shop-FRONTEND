using Microsoft.AspNetCore.Mvc;

namespace Online_Shoes_Shop.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
