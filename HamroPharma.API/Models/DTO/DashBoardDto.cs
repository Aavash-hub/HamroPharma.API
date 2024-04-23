namespace HamroPharma.API.Models.DTO
{
    public class DashboardDto
    {
        public decimal TotalSales { get; set; }
        public decimal TotalPurchases { get; set; }
        public int NumberOfCustomers { get; set; }
        public int NumberOfVendors { get; set; }
        public List<ExpiredDrugDto> ExpiredDrugs { get; set; }
    }


}
