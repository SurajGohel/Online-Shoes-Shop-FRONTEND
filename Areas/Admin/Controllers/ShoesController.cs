using Microsoft.AspNetCore.Mvc;

namespace Online_Shoes_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShoesController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
    }
}
