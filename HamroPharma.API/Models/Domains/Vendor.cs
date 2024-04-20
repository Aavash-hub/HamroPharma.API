namespace HamroPharma.API.Models.Domains
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string companyName { get; set; }
        public decimal? Balance { get; set; }
    }
}
