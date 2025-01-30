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
                //Console.WriteLine(userid);
                var response = await _httpClient.GetAsync($"ByUserId/{userid}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine("API Response Data: " + data); // Log the raw JSON response
                    var shoes = JsonConvert.DeserializeObject<List<CartModel>>(data);
                    //Console.WriteLine("Deserialized Shoes: " + JsonConvert.SerializeObject(shoes)); // Log the deserialized data
                    HttpContext.Session.SetString("TempTest","Data");

                    HttpContext.Session.SetString("CartItems", JsonConvert.SerializeObject(shoes));
                    return View( shoes);
                    //return View();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                //Console.WriteLine(ex.Message);
                HttpContext.Session.Remove("CartItems");
            }
            HttpContext.Session.Remove("CartItems");
            return View(Enumerable.Empty<ShoesModel>());
            //return PartialView("_ShoesList", Enumerable.Empty<ShoesModel>()); // Return an empty view if something goes wrong
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
