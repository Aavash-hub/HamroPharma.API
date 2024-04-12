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

        public async Task<Products> AddProduct(Products products, decimal vendorBalanceChange)
        {
            await dbcontext.Products.AddAsync(products);
            await dbcontext.SaveChangesAsync();

            return products;
        }

        public Task<Products> DeleteProducts(Products products)
        {
            throw new NotImplementedException();
        }

        public Task<Products> GetProducts(Products products)
        {
            throw new NotImplementedException();
        }

        public Task<Products> UpdateProducts(Products products)
        {
            throw new NotImplementedException();
        }
    }
}
