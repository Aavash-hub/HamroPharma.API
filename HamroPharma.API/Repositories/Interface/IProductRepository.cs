using HamroPharma.API.Models.Domains;

namespace HamroPharma.API.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<Products> UpdateProducts(Products products);
        Task<Products> GetProducts(Products products);
        Task <Products> DeleteProducts(Products products);
        Task<Products> AddProduct(Products product, decimal vendorBalanceChange);
    }
}
