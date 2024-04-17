using HamroPharma.API.Data;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HamroPharma.API.Repositories.Implementation
{
    public class VendorRepository : IVendorRespository
    {
        private readonly HPDbcontext _dbContext;

        public VendorRepository(HPDbcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vendor> AddAysnc(Vendor vendor)
        {
            await _dbContext.Vendors.AddAsync(vendor);
            await _dbContext.SaveChangesAsync();
            return vendor;
        }

        public async Task<Vendor?> DeleteAysnc(Vendor vendor)
        {
            var existingVendor = await _dbContext.Vendors.FindAsync(vendor.Id);
            if (existingVendor == null)
            {
                return null; // Vendor not found
            }

            _dbContext.Vendors.Remove(existingVendor);
            await _dbContext.SaveChangesAsync();
            return existingVendor;
        }

        public async Task<IEnumerable<Vendor>> GetAllAysnc()
        {
            return await _dbContext.Vendors.ToListAsync();
        }

        public async Task<Vendor> GetByIdAysnc(Guid id)
        {
            return await _dbContext.Vendors.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Vendor?> UpdateAysnc(Vendor vendor)
        {
            _dbContext.Entry(vendor).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return vendor;
        }
    }
}
