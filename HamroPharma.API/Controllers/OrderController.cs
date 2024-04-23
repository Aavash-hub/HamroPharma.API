using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Repositories;
using HamroPharma.API.Repositories.Interface;
using HamroPharma.API.Repositories.Implementation;
using HamroPharma.API.Models.DTO;
using HamroPharma.API.Models.DTOs;

namespace HamroPharma.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRespository _orderRepository;
        private readonly IProductRepository _prodcutRespository;

        public OrderController(IOrderRespository orderRepository,IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _prodcutRespository = productRepository;
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(List<OrderDetailDTO> orderItems)
        {
            try
            {
                decimal totalAmount = 0;

                foreach (var orderItem in orderItems)
                {
                    var product = await _prodcutRespository.GetProductById(orderItem.OrderproductsId);
                    if (product == null)
                    {
                        return BadRequest($"Product with ID {orderItem.OrderproductsId} does not exist.");
                    }
                    totalAmount += product.Price * orderItem.quantity;

                }

                // Create a new order
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    totalamount = totalAmount,
                    OrderDate = DateTime.Now
                };

                // Save the order
                var newOrder = await _orderRepository.AddOrderAsync(order);

                // Save order items
                foreach (var orderItem in orderItems)
                {
                    var product = await _prodcutRespository.GetProductById(orderItem.OrderproductsId);
                    var orderDetail = new OrderDetail
                    {
                        Id = Guid.NewGuid(),
                        OrderId = newOrder.Id,
                        OrderproductsId = orderItem.OrderproductsId,
                        ProductName = orderItem.ProductName,
                        quantity = orderItem.quantity,
                        price = product.Price
                    };
                    await _orderRepository.AddOrderDetailAsync(orderDetail);
                }

                return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(504, $"Error creating order: {ex.Message}");
            }
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

    }
}
