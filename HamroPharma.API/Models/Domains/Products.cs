namespace HamroPharma.API.Models.Domains
{
    public class Products
    {
        public Guid id { get; set; }

        public string? name { get; set; }

        public string? Description { get; set; }

        public string? Productprice { get; set; }

        public DateTime? addDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public bool IsExpirable { get; set; } = false;

        public DateTime? ModifiedDate { get; set; }

        public decimal MinQuantity { get; set; }

        public decimal MaxQuantity { get; set; }

        public decimal openingStock { get; set; }

        public decimal openingRate { get; set; }

    }
}
