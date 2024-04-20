using System.Globalization;

namespace HamroPharma.API.Models.Domains
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid productsId {  get; set; }
        public String ProductName { get; set; }
        public int? quantity { get; set; }
        public decimal price { get; set; }
        public Products Products { get; set; }

    }
}
