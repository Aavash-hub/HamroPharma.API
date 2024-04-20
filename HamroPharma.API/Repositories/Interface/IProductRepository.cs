using HamroPharma.API.Models.Domains;

namespace HamroPharma.API.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<Products?> UpdateProduct(Products products);
        Task<IEnumerable<Products>> GetAllAysnc();
        Task <Products?> DeleteProducts(Guid id);
        Task<Products> AddProduct(Products product);
        Task<Products> GetProductById(Guid id);
    }
}
