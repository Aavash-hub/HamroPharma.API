using HamroPharma.API.Models.Domains;

namespace HamroPharma.API.Repositories.Interface
{
    public interface IVendorRespository
    {
        Task<Vendor?> UpdateAysnc(Vendor vendor);
        Task<IEnumerable<Vendor>> GetAllAysnc();
        Task<Vendor?> DeleteAysnc(Vendor vendor);
        Task<Vendor> AddAysnc(Vendor vendor);
        Task<Vendor> GetByIdAysnc(Guid id);
    }
}
