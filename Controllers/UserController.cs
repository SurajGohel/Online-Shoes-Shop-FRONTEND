using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Shoes_Shop.Models;
using System.Text;

namespace Online_Shoes_Shop.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles ="User")]
        public IActionResult Index()
        {
            return View();
        }

        //private readonly IConfiguration _configuration;
        //private readonly HttpClient _httpClient;

        //public UserController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    _httpClient = new HttpClient
        //    {
        //        BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
        //    };
        //}

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(string username, string Email, string password)
        //{
        //    var payload = new
        //    {
        //        userName = username,
        //        email = Email,
        //        password = password
        //    };

        //    var jsonContent = JsonConvert.SerializeObject(payload);
        //    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync("api/Account/register", content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index", "Shoes");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Pratishodhhhhhhhhhhhhhhhhhhhh");
        //    }
        //    return View();

        //}

        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(string username, string password)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    var payload = new
        //    {
        //        userName = username,
        //        password = password
        //    };

        //    var jsonContent = JsonConvert.SerializeObject(payload);
        //    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync("api/Account/login", content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index","Shoes");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Pratishodhhhhhhhhhhhhhhhhhhhh");
        //    }
        //    return View();
        //}

    }
}
