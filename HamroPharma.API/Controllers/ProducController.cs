using Microsoft.AspNetCore.Mvc;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Models.DTO;
using HamroPharma.API.Repositories.Interface;
using System;
using System.Threading.Tasks;

namespace HamroPharma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductrequestDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Create Products entity
                var product = new Products
                {
                    name = productDto.Name,
                    Description = productDto.Description,
                    Quantity = productDto.Quantity,
                    Price = productDto.Price,
                    VednorId = productDto.VendorId
                };

                // Add product and update vendor balance
                await _productRepository.AddProduct(product, productDto.VendorBalanceChange);

                return Ok("Product added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

/*        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productRepository.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }*/
    }
}
