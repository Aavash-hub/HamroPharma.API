using HamroPharma.API.Data;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HamroPharma.API.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly HPDbcontext dbcontext;
        public ProductRepository(HPDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Products> AddProduct(Products products)
        {
           await dbcontext.Products.AddAsync(products);
            await dbcontext.SaveChangesAsync();

            return products;
        }

        public async Task<Products?> DeleteProducts(Guid id)
        {
            var existingProduct = await dbcontext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return null; // Product not found
            }
            dbcontext.Products.Remove(existingProduct);
            await dbcontext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<IEnumerable<Products>> GetAllAysnc()
        {
            return await dbcontext.Products.ToListAsync();
        }

        public async Task<Products?> GetProductById(Guid id)
        {
            return await dbcontext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);;
        }

        public async Task<Products?> UpdateProduct(Products products)
        {
            var existingProduct = await dbcontext.Products.FindAsync(products.Id);
            if (existingProduct != null)
            {
                dbcontext.Entry(existingProduct).State = EntityState.Detached; // Detach existing entity
            }
            dbcontext.Entry(products).State = EntityState.Modified;
            await dbcontext.SaveChangesAsync();
            return products;
        }
    }
}
