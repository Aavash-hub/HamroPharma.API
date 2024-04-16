namespace HamroPharma.API.Models.Domains
{
    public class Transcation
    {
        public Guid ID { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CustomerID { get; set; }
        public Customer Customer { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
