namespace HamroPharma.API.Models.DTO
{
    public class SalesReportItemDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public DateTime TransactionDate { get; set; }

    }
}
