using HamroPharma.API.Data;
using HamroPharma.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HamroPharma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly HPDbcontext _context;

        public DashboardController(HPDbcontext context)
        {
            _context = context;
        }

        [HttpGet("total-sales")]
        public async Task<ActionResult<decimal>> GetTotalSales()
        {
            decimal totalSales = await _context.Transcations
                .SumAsync(t => t.TotalAmount);
            return Ok(totalSales);
        }

        [HttpGet("total-purchases")]
        public async Task<ActionResult<decimal>> GetTotalPurchases()
        {
           decimal totalPurchases = await _context.Purchases
                .SumAsync(p => p.Totalamount);
            return Ok(totalPurchases);
        }

        [HttpGet("customer-count")]
        public async Task<ActionResult<int>> GetCustomerCount()
        {
            int numberOfCustomers = await _context.Customers.CountAsync();
            return Ok(numberOfCustomers);
        }

        [HttpGet("vendor-count")]
        public async Task<ActionResult<int>> GetVendorCount()
        {
            int numberOfVendors = await _context.Vendors.CountAsync();
            return Ok(numberOfVendors);
        }

        [HttpGet("expired-drugs")]
        public async Task<ActionResult<IEnumerable<ExpiredDrugDto>>> GetExpiredDrugs()
        {
            var expiredDrugs = await _context.Products
                .Where(p => p.ExpiryDate < DateTime.Today)
                .Select(p => new ExpiredDrugDto
                {
                    ProductName = p.Name,
                    ExpiryDate = p.ExpiryDate
                }).ToListAsync();
            return Ok(expiredDrugs);
        }
    }
}
