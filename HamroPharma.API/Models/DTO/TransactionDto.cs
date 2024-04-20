namespace HamroPharma.API.Models.DTO
{
    public class TransactionDto
    {
        public decimal Discount { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
    }
}
