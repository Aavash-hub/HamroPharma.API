using HamroPharma.API.Models.DTO;

namespace HamroPharma.API.Models.DTOs
{
    public class SalesReportDto
    {
        public string ProductName { get; set; }
        public int quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
