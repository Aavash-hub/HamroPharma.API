namespace HamroPharma.API.Models.Domains
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal totalamount { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

}
