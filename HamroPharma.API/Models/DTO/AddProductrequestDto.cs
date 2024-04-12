namespace HamroPharma.API.Models.DTO
{
    public class AddProductrequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public string Price { get; set; }
        public Guid VendorId { get; set; }
        public decimal VendorBalanceChange { get; set; }
    }
}
