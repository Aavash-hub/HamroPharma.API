using System.ComponentModel.DataAnnotations.Schema;

namespace HamroPharma.API.Models.Domains
{
    public class Products
    {
        public Guid id { get; set; }

        public string? name { get; set; }

        public string Description { get; set; }
        public int? Quantity { get; set; }

        public string? Price { get; set; }

        public  Guid VednorId {  get; set; }

        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }
    }
}
