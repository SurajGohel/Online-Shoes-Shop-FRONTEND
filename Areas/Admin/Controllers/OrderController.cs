using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Shoes_Shop.Models;
using System.Net.Http;

namespace Online_Shoes_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> GetAllOrders()
        {
            Console.WriteLine("Admin Gaya ::::::::::::::::");
            try
            {
                var response = await _httpClient.GetAsync($"GetAllUserOrders");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Successsssssssssssssssssssssssss ::::::::::::::::");

                    var data = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine("API Response Data: " + data); // Log the raw JSON response
                    var orders = JsonConvert.DeserializeObject<List<GetUserOrdersModel>>(data);
                    //Console.WriteLine("Deserialized Shoes: " + JsonConvert.SerializeObject(shoes)); // Log the deserialized data
                    return View(orders);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Catchhhhhhhhhhhhhhhhhhhhhhhhhhhhh ::::::::::::::::");

                // Log the exception
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Khali View Gyuuuuuuuuuuuuuuuuuuuuu ::::::::::::::::");

            return View(Enumerable.Empty<GetUserOrdersModel>()); // Return an empty view if something goes wrong
        }

        public async Task<IActionResult> OrderDetailInAdmin(int id)
        {
            var response = await _httpClient.GetAsync($"OrderByOrderId/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<GetOrderByOrderIdModel>(data);
                ViewBag.Order = order;

                var response2 = await _httpClient.GetAsync($"GetAllOrderByOrderId/{id}");
                if (response2.IsSuccessStatusCode)
                {
                    var data2 = await response2.Content.ReadAsStringAsync();
                    var orderDetails = JsonConvert.DeserializeObject<List<GetOrderDetailByOrderIdModel>>(data2);
                    return View(orderDetails);
                }
            }
            else
            {
                ViewBag.Order = new GetOrderByOrderIdModel();
            }

            return await GetAllOrders();
        }
    }
}
