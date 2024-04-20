using HamroPharma.API.Models.Domains;

namespace HamroPharma.API.Repositories.Interface
{
    public interface IOrderRespository
    {
        Task<Order> GetOrderByIdAsync(Guid id);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> AddOrderAsync(Order order);

    }
}
