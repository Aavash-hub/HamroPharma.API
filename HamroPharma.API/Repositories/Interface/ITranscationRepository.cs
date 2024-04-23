using HamroPharma.API.Models.Domains;

namespace HamroPharma.API.Repositories.Interface
{
    public interface ITranscationRepository
    {
        Task<Transcation> AddTransactionAsync(Transcation transaction);
        Task<Transcation> GetTransactionByIdAsync(Guid id);
        Task<List<Transcation>> GetAllTransactions();
    }
}
