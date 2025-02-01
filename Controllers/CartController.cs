using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Shoes_Shop.Models;
using System.Net.Http;

namespace Online_Shoes_Shop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public CartController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        [Authorize]
        public async Task<IActionResult> GetCartItem()
        {
            //Console.WriteLine("AAvo");
            try
            {
                string userid = Request.Cookies["userid"].ToString();
                var response = await _httpClient.GetAsync($"ByUserId/{userid}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var shoes = JsonConvert.DeserializeObject<List<CartModel>>(data);
                    return View(shoes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(Enumerable.Empty<ShoesModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int CartId)
        {
            var response = await _httpClient.DeleteAsync($"api/Cart/{CartId}");
            return RedirectToAction("GetCartItem");
        }

        //[HttpGet]
        //public  IActionResult GoToCheckOut(int CartId)
        //{
        //    //var response = await _httpClient.DeleteAsync($"api/Cart/{CartId}");
        //    return View("Index", "Order");
        //}

        
        public IActionResult GoToCheckOut()
        {
            return RedirectToAction("Index", "Order");
        }
    }
}
