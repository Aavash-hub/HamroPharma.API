using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Order")]
        public Guid? TranscationOrderId { get; set; }
        public Order Order { get; set; }

    }
}
