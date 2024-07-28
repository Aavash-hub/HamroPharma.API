using HamroPharma.API.Models.DTO;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using HamroPharma.API.Repositories.Implementation;
using HamroPharma.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HamroPharma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranscationController : Controller
    {

        private readonly ITranscationRepository _transactionRepository;
        private readonly IOrderRespository _orderRepository;
        private readonly IcustomerRepository _customerRepository;

        public TranscationController(ITranscationRepository transactionRepository, IOrderRespository oderRespository, IcustomerRepository customerRepository)
        {
            _transactionRepository = transactionRepository;
            _orderRepository = oderRespository;
            _customerRepository = customerRepository;
        }

        [HttpPost("{orderId}")]
        public async Task<ActionResult<Transcation>> AddTransaction(Guid orderId, TransactionDto transactionDto)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return NotFound($"Order with ID {orderId} not found.");
                }

                var customer = await _customerRepository.GetByIdAysnc(transactionDto.CustomerId);
                if (customer == null)
                {
                    return NotFound($"Customer with ID {transactionDto.CustomerId} not found.");
                }

                decimal totalBeforeDiscount = order.totalamount; // Assuming this is the pre-discount total
                decimal finalAmount = totalBeforeDiscount - transactionDto.Discount;

                // These lines assume you're tracking paid amounts or adjustments to the original total
                order.totalamount -= finalAmount; // Be careful with this line, as it might not be doing what you expect
                customer.CustomerBalance += finalAmount;

                var transaction = new Transcation
                {
                    ID = Guid.NewGuid(),
                    discount = transactionDto.Discount,
                    TransactionDate = transactionDto.PurchaseDate,
                    TotalAmount = finalAmount,
                    CustomerID = transactionDto.CustomerId,
                    TranscationOrderId = orderId
                };

                var newTransaction = await _transactionRepository.AddTransactionAsync(transaction);
                await _customerRepository.UpdateAysnc(customer);

                return CreatedAtAction(nameof(GetTransaction), new { id = newTransaction.ID }, newTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/transactions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Transcation>> GetTransaction(Guid id)
        {
            try
            {
                var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
                if (transaction == null)
                {
                    return NotFound();
                }
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving transaction: {ex.Message}");
            }
        }

        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<SalesReportDto>>> GetSalesReport()
        {
            try
            {
                var transactions = await _transactionRepository.GetAllTransactions();

                // Debugging: Check if customers are loaded
                var debugCustomers = transactions.Select(t => t.Customer?.Name ?? "No Customer Loaded").ToList();
                Console.WriteLine(string.Join(", ", debugCustomers));

                var salesReport = transactions.SelectMany(t => t.Order.OrderDetails.Select(od => new SalesReportDto
                {
                    ProductName = od.ProductName ?? "Unknown",
                    quantity = od.quantity ?? 0,
                    TotalAmount = t.TotalAmount,
                    CustomerName = t.Customer?.Name ?? "Unknown",
                    TransactionDate = t.TransactionDate
                })).ToList();

                return Ok(salesReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generating sales report: {ex.Message}");
            }
        }

    }
}
