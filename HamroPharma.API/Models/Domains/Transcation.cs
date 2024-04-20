namespace HamroPharma.API.Models.Domains
{
    public class Transcation
    {
        public Guid ID { get; set; }
        public decimal discount { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid CustomerID { get; set; }
        public Customer Customer { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
