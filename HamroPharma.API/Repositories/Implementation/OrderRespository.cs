using HamroPharma.API.Data;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HamroPharma.API.Repositories.Implementation
{
    public class OrderRespository : IOrderRespository
    {
        private readonly HPDbcontext _context;

        public OrderRespository(HPDbcontext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Products)
                .SingleOrDefaultAsync(o => o.Id == id);
        }
    }
}
