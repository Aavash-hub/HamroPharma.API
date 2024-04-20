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
        public async Task<ActionResult<Order>> CreateOrder(List<OrderDetailDTO> orderDetails)
        {
            try
            {
                // Calculate total amount for the order
                decimal totalAmount = 0;
                foreach (var orderDetail in orderDetails)
                {
                    var product = await _prodcutRespository.GetProductById(orderDetail.ProductId);
                    if (product != null)
                    {
                        totalAmount += (orderDetail.Price * orderDetail.Quantity);

                        // Deduct the selected product quantity from inventory
                        product.Quantity -= orderDetail.Quantity;
                        // Save the changes to the product quantity
                        await _prodcutRespository.UpdateProduct(product);
                    }
                }

                // Create a new order
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    totalamount = totalAmount,
                    OrderDate = DateTime.Now,
                    OrderDetails = ConvertToOrderDetails(orderDetails)
                };

                // Save the order
                var newOrder = await _orderRepository.AddOrderAsync(order);
                return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating order: {ex.Message}");
            }
        }

        private List<OrderDetail> ConvertToOrderDetails(List<OrderDetailDTO> orderDetailDTOs)
        {
            var orderDetails = new List<OrderDetail>();
            foreach (var dto in orderDetailDTOs)
            {
                orderDetails.Add(new OrderDetail
                {
                    Id = Guid.NewGuid(),
                    productsId = dto.ProductId,
                    quantity = dto.Quantity,
                    Products = new Products
                    {
                        Id = dto.ProductId,
                        Name = dto.ProductName,
                        Price = dto.Price
                    }
                });
            }
            return orderDetails;
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
