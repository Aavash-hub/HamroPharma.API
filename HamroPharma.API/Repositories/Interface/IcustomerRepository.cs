using HamroPharma.API.Models.Domains;

namespace HamroPharma.API.Repositories.Interface
{
    public interface IcustomerRepository
    {
        Task<Customer?> UpdateAysnc(Customer customer);
        Task<IEnumerable<Customer>> GetAllAysnc();
        Task<Customer?> DeleteAysnc(Customer customer);
        Task<Customer> AddAysnc(Customer customer);
        Task<Customer> GetByIdAysnc(Guid id);
    }
}
