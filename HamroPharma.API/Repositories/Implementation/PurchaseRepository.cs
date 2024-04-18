using HamroPharma.API.Data;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Models.DTOs;
using HamroPharma.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HamroPharma.API.Repositories.Implementation
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly HPDbcontext _dbContext;

        public PurchaseRepository(HPDbcontext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Purchase>> GetAllAsync()
        {
            return await _dbContext.Purchases.ToListAsync();
        }

        public async Task<Purchase> GetByIdAsync(Guid id)
        {
            return await _dbContext.Purchases.FindAsync(id);
        }

        public async Task<IEnumerable<PurchaseReportDto>> GetPurchaseReportAsync()
        {
            var purchaseReport = await(from purchase in _dbContext.Purchases
                                       join vendor in _dbContext.Vendors on purchase.VendorId equals vendor.Id
                                       join product in _dbContext.Products on purchase.ProductsId equals product.Id
                                       select new PurchaseReportDto
                                       {
                                           VendorName = vendor.Name,
                                           ProductName = product.Name,
                                           Quantity = (int)purchase.Quantity,
                                           Price = purchase.Price,
                                           purchasedate = purchase.purchasedate
                                       }).ToListAsync();

            return purchaseReport;
        }

        public async Task<Purchase> MakePurchaseAsync(Purchase purchase)
        {
            _dbContext.Purchases.Add(purchase);
            await _dbContext.SaveChangesAsync();
            return purchase;
        }

    }
}
