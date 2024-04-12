using System.ComponentModel.DataAnnotations.Schema;

namespace HamroPharma.API.Models.Domains
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId {get; set;}
        public Guid productId { get; set;}
        public int Quantity { get; set;}
        public decimal UnitPrice { get; set;}
        public decimal TotalPrice { get; set;}

        [ForeignKey("OrderId")]
        public Order? Order { get; set;}

        [ForeignKey("ProductId")]
        public Products? Products { get; set;}


    }
}
