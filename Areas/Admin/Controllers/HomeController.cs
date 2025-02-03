using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Shoes_Shop.Models; // <-- Add this
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Online_Shoes_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var statisticsResponse = await _httpClient.GetAsync("api/Dashboard/stats");
                var statistics = new DashboardStatisticsViewModel();
                if (statisticsResponse.IsSuccessStatusCode)
                {
                    var statisticsData = await statisticsResponse.Content.ReadAsStringAsync();
                    statistics = JsonConvert.DeserializeObject<DashboardStatisticsViewModel>(statisticsData);
                }

                var topSellingResponse = await _httpClient.GetAsync("api/Dashboard/top-selling");
                var topSellingShoes = new List<TopSellingShoesViewModel>();
                if (topSellingResponse.IsSuccessStatusCode)
                {
                    var topSellingData = await topSellingResponse.Content.ReadAsStringAsync();
                    topSellingShoes = JsonConvert.DeserializeObject<List<TopSellingShoesViewModel>>(topSellingData);
                }

                var lowStockResponse = await _httpClient.GetAsync("api/Dashboard/low-stock");
                var lowStockAlerts = new List<LowStockViewModel>();
                if (lowStockResponse.IsSuccessStatusCode)
                {
                    var lowStockData = await lowStockResponse.Content.ReadAsStringAsync();
                    lowStockAlerts = JsonConvert.DeserializeObject<List<LowStockViewModel>>(lowStockData);
                }

                var salesReportRes = await _httpClient.GetAsync("api/Dashboard/sales-data");
                var salesReportMonthly = new List<SalesDataModel>();
                if (salesReportRes.IsSuccessStatusCode)
                {
                    var sr = await salesReportRes.Content.ReadAsStringAsync();
                    salesReportMonthly = JsonConvert.DeserializeObject<List<SalesDataModel>>(sr);
                }

                var dashboardViewModel = new DashboardViewModel
                {
                    Statistics = statistics,
                    TopSellingShoes = topSellingShoes,
                    LowStockAlerts = lowStockAlerts,
                    SalesDataModels = salesReportMonthly
                };

                return View(dashboardViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching dashboard data: {ex.Message}");
                return View(new DashboardViewModel());
            }
        }
    }
}
