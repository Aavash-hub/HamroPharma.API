using HamroPharma.API.Data;
using HamroPharma.API.Models.Domains;
using HamroPharma.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace HamroPharma.API.Repositories.Implementation
{
    public class CustomerRepository : IcustomerRepository
    {
        private readonly HPDbcontext _dbContext;

        public CustomerRepository(HPDbcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> AddAysnc(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> DeleteAysnc(Customer customer)
        {
            var existingCustomer = await _dbContext.Customers.FindAsync(customer.Id);
            if (existingCustomer == null)
            {
                return null; // Customer not found
            }

            _dbContext.Customers.Remove(existingCustomer);
            await _dbContext.SaveChangesAsync();
            return existingCustomer;
        }

        public async Task<IEnumerable<Customer>> GetAllAysnc()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAysnc(Guid id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer?> UpdateAysnc(Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return customer;
        }
    }
}
