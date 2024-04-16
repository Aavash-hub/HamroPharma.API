using HamroPharma.API.Models.Domains;

namespace HamroPharma.API.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<Products> UpdateProducts(Products products);
        Task<IEnumerable<Products>> GetAllAysnc();
        Task <Products> DeleteProducts(Products products);
        Task<Products> AddProduct(Products product);
        Task<Products> GetProductById(Guid id);
    }
}
