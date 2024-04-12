namespace HamroPharma.API.Models.DTO
{
    public class ProductDto
    {
        public Guid id { get; set; }

        public string? name { get; set; }

        public string Description { get; set; }
        public string? Quantity { get; set; }

        public string? Price { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsExpirable { get; set; } = false;

        public decimal openingStock { get; set; }
    }
}