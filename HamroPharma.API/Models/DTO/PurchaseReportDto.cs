namespace HamroPharma.API.Models.DTOs
{
    public class PurchaseReportDto
    {
        public string VendorName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateOnly purchasedate { get; set; }
    }
}
