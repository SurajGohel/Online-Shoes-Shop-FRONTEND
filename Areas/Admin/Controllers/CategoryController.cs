using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Online_Shoes_Shop.Models;
using System.Net.Http;
using System.Text;

namespace Online_Shoes_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public CategoryController(IConfiguration configuration)
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
            var response = await _httpClient.GetAsync("api/Categories");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var shoes = JsonConvert.DeserializeObject<List<Online_Shoes_Shop.Models.CategoryDropdownModel>>(data);

                return View(shoes);
            }
            return View(new List<CategoryDropdownModel>());
        }

        public async Task<IActionResult> Add(int? CategoryId)
        {
            if (CategoryId.HasValue)
            {
                var response = await _httpClient.GetAsync($"CategoryDetail/{CategoryId}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var cat = JsonConvert.DeserializeObject<Online_Shoes_Shop.Models.CategoryDropdownModel>(data);
                    return View(cat);
                }
            }
            return View(new Online_Shoes_Shop.Models.CategoryDropdownModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(Online_Shoes_Shop.Models.CategoryDropdownModel cat)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(cat);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                Console.WriteLine(content);
                if (cat.CategoryId == null)
                {
                    response = await _httpClient.PostAsync("api/Categories", content);
                }
                else
                {
                    response = await _httpClient.PutAsync($"api/Categories/{cat.CategoryId}", content);
                }

                if (response.IsSuccessStatusCode)
                {
                    TempData["CategoryAdded"] = "Category Added";

                    return RedirectToAction("GetAll");
                }
            }
            return View("Add", cat);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int CategoryId)
        {
            Console.WriteLine($"{CategoryId}");
            var response = await _httpClient.DeleteAsync($"api/Categories/{CategoryId}");
            return RedirectToAction("GetAll");
        }
    }
}
