using HamroPharma.API.Data;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HamroPharma.API.Repositories.Implementation
{
    public class TranscationRespository : ITranscationRepository
    {
        private readonly HPDbcontext _context;

        public TranscationRespository(HPDbcontext context)
        {
            _context = context;
        }
        public async Task<Transcation> AddTransactionAsync(Transcation transaction)
        {
            _context.Transcations.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transcation> GetTransactionByIdAsync(Guid id)
        {
            return await _context.Transcations.FirstOrDefaultAsync(t => t.ID == id);
        }
    }
}
