using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Online_Shoes_Shop.Models;

namespace Online_Shoes_Shop.Controllers
{
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

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Shoes");
            if (response.IsSuccessStatusCode)
            {
                var res = await _httpClient.GetAsync("api/Categories");

                if (response.IsSuccessStatusCode)
                {
                    //Console.WriteLine("Thyu To Chhe");
                    var dt = await res.Content.ReadAsStringAsync();
                    var cat = JsonConvert.DeserializeObject<List<CategoriesModel>>(dt);

                    // Store in ViewBag for the dropdown
                    ViewBag.ShoesDropdown = cat.Select(shoe => new SelectListItem
                    {
                        Value = shoe.CategoryId.ToString(),
                        Text = shoe.CategoryName.ToString()
                    }).ToList();
                }
                else
                {
                    ViewBag.ShoesDropdown = new List<CategoriesModel>(); // Empty list if API call fails
                }

                var data = await response.Content.ReadAsStringAsync();
                var shoes = JsonConvert.DeserializeObject<List<ShoesModel>>(data);
                return View(shoes);
            }
            return View(new List<ShoesModel>());
        }

        public async Task<IActionResult> ShoeDetail(int ShoeId)
        {
            //Console.WriteLine("::::::::::: +" + ShoeId);
            try
            {
                // Call the API to fetch shoe details by ID
                var response = await _httpClient.GetAsync($"ShoeDetail/{ShoeId}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var shoe = JsonConvert.DeserializeObject<ShoeDetailModel>(data);

                    //Console.WriteLine(shoe);
                    // Return the ShoeDetail view with the retrieved shoe data
                    return View(shoe);
                }
                else
                {
                    // Handle cases where the API response is unsuccessful
                    ViewBag.ErrorMessage = "Unable to retrieve shoe details.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception and show a generic error message
                //Console.WriteLine(ex.Message);
                ViewBag.ErrorMessage = "An error occurred while retrieving shoe details.";
            }

            // Return the view with an empty model in case of error
            return View(new ShoeDetailModel());
        }



        public async Task<IActionResult> SearchPartial(string query)
        {
            //Console.WriteLine(query);
            if (string.IsNullOrWhiteSpace(query))
            {
                return PartialView("_ShoesList", Enumerable.Empty<ShoesModel>());
            }

            try
            {
                // Call the Web API to search for shoes
                using var client = new HttpClient();
                var response = await _httpClient.GetAsync($"api/Shoes/SearchByName?shoeName={query}");

                if (!response.IsSuccessStatusCode)
                {
                    // Handle cases like NotFound, BadRequest, etc.
                    return PartialView("_ShoesList", Enumerable.Empty<ShoesModel>());
                }

                // Deserialize the response content
                var shoes = await response.Content.ReadFromJsonAsync<IEnumerable<ShoesModel>>();
                return PartialView("_ShoesList", shoes);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown for simplicity)
                return PartialView("_ShoesList", Enumerable.Empty<ShoesModel>());
            }
        }

        public async Task<IActionResult> GetShoesByCategory(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"ByCategory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine("API Response Data: " + data); // Log the raw JSON response
                    var shoes = JsonConvert.DeserializeObject<List<ShoesModel>>(data);
                    //Console.WriteLine("Deserialized Shoes: " + JsonConvert.SerializeObject(shoes)); // Log the deserialized data
                    return PartialView("_ShoesList", shoes);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                //Console.WriteLine(ex.Message);
            }

            return PartialView("_ShoesList", Enumerable.Empty<ShoesModel>()); // Return an empty view if something goes wrong
        }
    }
}

//we need
//ShoesModel table
//+
//review
//+
//user
//+