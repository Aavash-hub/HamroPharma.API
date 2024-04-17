using Microsoft.AspNetCore.Mvc;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Models.DTO;
using HamroPharma.API.Repositories.Interface;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HamroPharma.API.Data;
using HamroPharma.API.Repositories.Implementation;
using Microsoft.AspNetCore.Http.HttpResults;
using HamroPharma.API.Models.DTOs;

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

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetAllProducts()
        {
            try
            {
                var products = await _productRepository.GetAllAysnc();
                return Ok(products);
            }
            catch
            {
                return StatusCode(500, "Failed to retrieve products");
            }
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Products>> AddProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var product = new Products
                {
                    Id = Guid.NewGuid(),
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Quantity = productDto.Quantity,
                    CreatedDate = productDto.CreatedDate,
                    Price = productDto.Price,
                    ExpiryDate = productDto.ExpiryDate
                };

                var addedProduct = await _productRepository.AddProduct(product);
                return CreatedAtAction(nameof(GetProduct), new { id = addedProduct.Id }, addedProduct);
            }
            catch
            {
                return StatusCode(500, "Failed to add the product");
            }
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(Guid id)
        {
            var product = await _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        //put: api/product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(Guid id, ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var existingProduct = await _productRepository.GetProductById(id);

                if (existingProduct == null)
                {
                    return NotFound("Product not found");
                }

                existingProduct.Name = productDto.Name;
                existingProduct.Description = productDto.Description;
                existingProduct.Quantity = productDto.Quantity;
                existingProduct.CreatedDate = productDto.CreatedDate;
                existingProduct.Price = productDto.Price;
                existingProduct.ExpiryDate = productDto.ExpiryDate;

                await _productRepository.UpdateProduct(existingProduct);

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Failed to update the product");
            }
        }

        // DELETE: api/products/
        [HttpDelete]
        public async Task<IActionResult> DeleteProducts(Products products)
        {
            try
            {
                var deletedProduct = await _productRepository.DeleteProducts(products);

                if (deletedProduct != null)
                {
                    return NoContent(); // Deletion successful
                }
                else
                {
                    return NotFound(); // Product not found
                }
            }
            catch
            {
                return StatusCode(500, "Failed to delete the product"); // Deletion failed due to exception
            }
        }

    }
}

