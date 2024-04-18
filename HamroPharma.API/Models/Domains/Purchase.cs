namespace HamroPharma.API.Models.Domains
{
    public class Purchase
    {
        internal DateTime Purchasedate;

        public Guid Id { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public DateOnly purchasedate { get; set; }
        public decimal Totalamount { get; set; }
        public Guid VendorId { get; set; }
        public Guid ProductsId { get; set; }
        public Vendor Vendor { get; set; }
        public Products products { get; set; }
    }
}
