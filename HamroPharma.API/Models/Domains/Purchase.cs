namespace HamroPharma.API.Models.Domains
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public DateOnly purchasdate { get; set; }
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public Guid ProductId { get; set; }
        public Products products { get; set; }
    }
}
