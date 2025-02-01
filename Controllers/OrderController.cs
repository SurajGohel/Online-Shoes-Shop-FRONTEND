using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Shoes_Shop.Models;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;



namespace Online_Shoes_Shop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }
        public async Task<IActionResult> Index()
        {
            await LoadCartItems();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderModel order)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Checkout", content);


                if (response.IsSuccessStatusCode)
                {
                    TempData["OrderSuccessMessage"] = "Order placed successfully!";
                    return RedirectToAction("GetCartItem", "Cart");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Registration failed: {errorResponse}");
                }
            }
            await LoadCartItems();
            return View("Index",order);

        }

        public async Task LoadCartItems()
        {
            string userid = Request.Cookies["userid"]; // Ensure it doesn't throw NullReferenceException

            if (string.IsNullOrEmpty(userid))
            {
                ViewBag.CartInfo = new List<CartModel>(); // Return an empty list to avoid errors
                return;
            }

            var response = await _httpClient.GetAsync($"ByUserId/{userid}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var cartItems = JsonConvert.DeserializeObject<List<CartModel>>(data);
                ViewBag.CartInfo = cartItems;
            }
            else
            {
                ViewBag.CartInfo = new List<CartModel>(); // Ensure ViewBag is not null
            }
        }

    }
}
