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

    }
}
