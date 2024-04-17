namespace HamroPharma.API.Models.DTOs
{
    public class PurchaseDto
    {
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid VendorId { get; set; }
        public Guid ProductId { get; set; }
    }
}
