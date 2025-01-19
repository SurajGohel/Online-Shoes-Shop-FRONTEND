using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Online_Shoes_Shop.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
