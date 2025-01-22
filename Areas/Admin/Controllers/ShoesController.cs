using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Shoes_Shop.Controllers;
using Online_Shoes_Shop.Models;
using System.Text;

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

        public async Task<IActionResult> Add(int? ShoeId)
        {
            await LoadCategoryDropdown();
            Console.WriteLine(ShoeId);

            if (ShoeId.HasValue)
            {
                var response = await _httpClient.GetAsync($"ShoeByPK/{ShoeId}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var shoe = JsonConvert.DeserializeObject<ShoesModel>(data);
                    return View(shoe);
                }
            }
            return View(new ShoesModel());
        }

        private async Task LoadCategoryDropdown()
        {
            var response = await _httpClient.GetAsync("api/Categories");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var cats = JsonConvert.DeserializeObject<List<CategoryDropdownModel>>(data);
                ViewBag.CategoryList = cats;
            }   
        }

        //[HttpPost]
        //public async Task<IActionResult> Save(ShoesModel shoe)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var json = JsonConvert.SerializeObject(shoe);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response;
        //        if (shoe.ShoeId == null)
        //        {
        //            response = await _httpClient.PostAsync("api/Shoes", content);
        //        }
        //        else
        //        {
        //            response = await _httpClient.PutAsync($"api/Shoes/{shoe.ShoeId}", content);
        //        }

        //        if (response.IsSuccessStatusCode)
        //            return RedirectToAction("GetAll");
        //    }
        //    return View("Add", shoe);
        //}

        [HttpPost]
        public async Task<IActionResult> Save([FromForm] ShoesModel shoe)
        {

            if (!ModelState.IsValid)
            {
                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent(shoe.Name), "Name");
                formData.Add(new StringContent(shoe.CategoryId.ToString()), "CategoryId");
                formData.Add(new StringContent(shoe.Price.ToString()), "Price");
                formData.Add(new StringContent(shoe.Image.ToString()), "ImageURL");
                formData.Add(new StringContent(shoe.Description), "Description");
                formData.Add(new StringContent(shoe.Stock.ToString()), "Stock");

                if (shoe.Image != null)
                {
                    var fileContent = new StreamContent(shoe.Image.OpenReadStream());
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(shoe.Image.ContentType);
                    formData.Add(fileContent, "Image", shoe.Image.FileName);
                }

                HttpResponseMessage response;
                if (shoe.ShoeId == null)
                {

                    response = await _httpClient.PostAsync("api/Shoes", formData);
                }
                else
                {
                    formData.Add(new StringContent(shoe.ShoeId.ToString()), "ShoeId");

                    response = await _httpClient.PutAsync($"api/Shoes/{shoe.ShoeId}", formData);
                }
                string errorDetails = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {errorDetails}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("GetAll");
            }
            await LoadCategoryDropdown();

            return View("Add", shoe);
        }

        

        [HttpGet]
        public async Task<IActionResult> Delete(int ShoeId)
        {
            var response = await _httpClient.DeleteAsync($"api/Shoes/{ShoeId}");
            return RedirectToAction("GetAll");
        }
    }
}
