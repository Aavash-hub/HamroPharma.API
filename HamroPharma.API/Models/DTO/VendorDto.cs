namespace HamroPharma.API.Models.DTOs
{
    public class VendorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string CompanyName { get; set; }
        public decimal? Balance { get; set; }
    }
}
