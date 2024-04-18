using HamroPharma.API.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace HamroPharma.API.Data
{
    public class HPDbcontext : DbContext
    {
        public HPDbcontext(DbContextOptions<HPDbcontext> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        /*       public DbSet<OrderItem> OrderItems { get; set; }
               public DbSet<Transcation> Transcations { get; set;}*/

        /* public DbSet<Order> Orders { get; set; }
 */

    }
}
