using HamroPharma.API.Models.DTO;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HamroPharma.API.Controllers
{
    public class TranscationController : Controller
    {
        private readonly ITranscationRepository _transactionRepository;

        public TranscationController(ITranscationRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // POST: api/transactions
        [HttpPost]
        public async Task<ActionResult<Transcation>> AddTransaction(TransactionDto transactionDto)
        {
            try
            {
                var transaction = new Transcation
                {
                    ID = Guid.NewGuid(),
                    discount = transactionDto.Discount,
                    TransactionDate = transactionDto.TransactionDate,
                    TotalAmount = transactionDto.TotalAmount,
                    CustomerID = transactionDto.CustomerId,
                    OrderId = transactionDto.OrderId
                };

                var newTransaction = await _transactionRepository.AddTransactionAsync(transaction);
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
    }
}
