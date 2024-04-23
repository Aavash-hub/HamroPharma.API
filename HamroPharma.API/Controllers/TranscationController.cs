using HamroPharma.API.Models.DTO;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using HamroPharma.API.Repositories.Implementation;
using HamroPharma.API.Models.DTOs;

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

        // POST: api/transactions
        [HttpPost("add")]
        public async Task<ActionResult<Transcation>> AddTransaction(TransactionDto transactionDto)
        {
            decimal discountAmount = 0;
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(transactionDto.TranscationOrderId);
                var customer = await _customerRepository.GetByIdAysnc(transactionDto.CustomerId);
                discountAmount = transactionDto.Discount;
                decimal discountedTotalAmount = transactionDto.TotalAmount - discountAmount;
                order.totalamount -= discountedTotalAmount;
                customer.CustomerBalance += discountedTotalAmount;

                var transaction = new Transcation
                {
                    ID = Guid.NewGuid(),
                    discount = discountAmount,
                    TransactionDate = DateTime.Now,
                    TotalAmount = transactionDto.TotalAmount,
                    CustomerID = transactionDto.CustomerId,
                    TranscationOrderId = transactionDto.TranscationOrderId
                };

                var newTransaction = await _transactionRepository.AddTransactionAsync(transaction);
                await _customerRepository.UpdateAysnc(customer);

                return CreatedAtAction(nameof(GetTransaction), new { id = newTransaction.ID }, newTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding transaction: {ex.Message}");
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

                var salesReport = transactions.SelectMany(t => t.Order.OrderDetails.Select(od => new SalesReportDto
                {
                    ProductName = od.ProductName ?? "Unknown", // The property name used here must match your class definition.
                    quantity = od.quantity ?? 0, // The property name used here must match your class definition.
                    TotalAmount = t.TotalAmount, // Assuming this is correct.
                    CustomerName = t.Customer?.Name ?? "Unknown", // Assuming this is correct.
                    TransactionDate = t.TransactionDate // Assuming this is correct.
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
