namespace Online_Shoes_Shop.Models
{
    public class DashboardViewModel
    {
        public DashboardStatisticsViewModel Statistics { get; set; }
        public List<TopSellingShoesViewModel> TopSellingShoes { get; set; }
        public List<LowStockViewModel> LowStockAlerts { get; set; }

        public List<SalesDataModel> SalesDataModels { get; set; }
    }

    public class SalesDataModel
    {
        public string Month { get; set; }
        public decimal TotalSales { get; set; }
    }

    public class DashboardStatisticsViewModel
    {
        public int TotalOrders { get; set; }
        public int TotalVisitors { get; set; }
        public decimal TotalSales { get; set; }
    }

    public class TopSellingShoesViewModel
    {
        public string Name { get; set; }
        public int TotalSold { get; set; }
    }

    public class LowStockViewModel
    {
        public string Name { get; set; }
        public int Stock { get; set; }
    }
}
