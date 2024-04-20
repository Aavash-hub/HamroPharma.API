using System.ComponentModel.DataAnnotations.Schema;

namespace HamroPharma.API.Models.Domains
{
    public class Products
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
