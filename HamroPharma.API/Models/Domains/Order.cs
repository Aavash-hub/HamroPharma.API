﻿namespace HamroPharma.API.Models.Domains
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid ProductsId { get; set; }
        public Products products { get; set; }
    }

}
