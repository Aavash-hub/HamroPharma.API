﻿namespace HamroPharma.API.Models.DTO
{
    public class TransactionDto
    {
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
