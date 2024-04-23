namespace HamroPharma.API.Models.DTOs
{
    public class OrderDetailDTO
    {
        public Guid OrderproductsId { get; set; }
        public Guid OrderId {  get; set; }
        public string ProductName { get; set; }
        public int quantity { get; set; }
        public decimal Price { get; set; }
    }
}
