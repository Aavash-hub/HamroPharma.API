using Microsoft.AspNetCore.Identity;

namespace HamroPharma.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string createJwtToken(IdentityUser user, List<string> roles);
    }
}
