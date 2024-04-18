using HamroPharma.API.Models.Domains;
using HamroPharma.API.Models.DTOs;

namespace HamroPharma.API.Repositories.Interface
{
    public interface IPurchaseRepository
    {
        Task<Purchase> MakePurchaseAsync(Purchase purchase);
        Task<IEnumerable<Purchase>> GetAllAsync();
        Task<Purchase> GetByIdAsync(Guid id);
        Task<IEnumerable<PurchaseReportDto>> GetPurchaseReportAsync();
    }
}
