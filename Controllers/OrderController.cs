﻿using Microsoft.AspNetCore.Mvc;
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

        //public IActionResult UserOrders()
        //{
        //    return View();
        //}

        public async Task<IActionResult> UserOrders()
        {
            try
            {
                string userid = Request.Cookies["userid"]; // Ensure it doesn't throw NullReferenceException

                var response = await _httpClient.GetAsync($"GetUserOrders/{userid}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine("API Response Data: " + data); // Log the raw JSON response
                    var orders = JsonConvert.DeserializeObject<List<GetUserOrdersModel>>(data);
                    //Console.WriteLine("Deserialized Shoes: " + JsonConvert.SerializeObject(shoes)); // Log the deserialized data
                    return View(orders);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
            }

            return View(Enumerable.Empty<GetUserOrdersModel>()); // Return an empty view if something goes wrong
        }

        public async Task<IActionResult> OrderDetail(int id)
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

            return await UserOrders();
        }

    }
}
