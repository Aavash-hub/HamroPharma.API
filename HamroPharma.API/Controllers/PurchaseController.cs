using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Models.DTOs;
using HamroPharma.API.Repositories.Interface;
using HamroPharma.API.Repositories.Implementation;

namespace HamroPharma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IVendorRespository _vendorRepository;

        public PurchaseController(IPurchaseRepository purchaseRepository, IProductRepository productRepository, IVendorRespository vendorRepository)
        {
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
            _vendorRepository = vendorRepository;
        }

        // Existing actions

        [HttpPost]
        public async Task<ActionResult<Purchase>> MakePurchase(PurchaseDto purchaseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Retrieve product and vendor details
                var product = await _productRepository.GetProductById(purchaseDto.ProductId);
                if (product == null)
                {
                    return NotFound("Product not found");
                }

                var vendor = await _vendorRepository.GetByIdAysnc(purchaseDto.VendorId);
                if (vendor == null)
                {
                    return NotFound("Vendor not found");
                }

                // Calculate total amount
                decimal totalAmount = (decimal)(purchaseDto.Quantity * purchaseDto.Price);

                // Update vendor balance
                vendor.Balance += totalAmount;

                // Update product quantity
                product.Quantity += purchaseDto.Quantity;

                // Create purchase object
                var purchase = new Purchase
                {
                    Quantity = purchaseDto.Quantity,
                    Price = purchaseDto.Price,
                    Purchasedate = purchaseDto.PurchaseDate,
                    Totalamount = totalAmount,
                    VendorId = purchaseDto.VendorId,
                    ProductsId = purchaseDto.ProductId
                };

                // Save changes to database
                await _purchaseRepository.MakePurchaseAsync(purchase);

                return CreatedAtAction(nameof(GetPurchase), new { id = purchase.Id }, purchase);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to make the purchase: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchase(Guid id)
        {
            try
            {
                var purchase = await _purchaseRepository.GetByIdAsync(id);
                if (purchase == null)
                {
                    return NotFound();
                }

                return Ok(purchase);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve the purchase: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseReportDto>>> GetPurchaseReport()
        {
            try
            {
                var purchaseReport = await _purchaseRepository.GetPurchaseReportAsync();
                return Ok(purchaseReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve purchase report: {ex.Message}");
            }
        }
    }
}
