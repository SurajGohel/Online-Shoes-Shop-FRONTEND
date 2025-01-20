using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Shoes_Shop.Controllers;
using Online_Shoes_Shop.Models;

namespace Online_Shoes_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShoesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ShoesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _httpClient.GetAsync("api/Shoes");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var shoes = JsonConvert.DeserializeObject<List<ShoesModel>>(data);
                return View(shoes);
            }
            return View(new List<ShoesModel>());
        }
    }
}
