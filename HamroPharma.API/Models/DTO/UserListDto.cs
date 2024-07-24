using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace HamroPharma.API.Models.DTO
{
    public class UserListDto
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

}
