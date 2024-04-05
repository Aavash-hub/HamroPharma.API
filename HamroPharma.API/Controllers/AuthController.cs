using HamroPharma.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HamroPharma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        //POST: {apibaseurl}/api/auth/adduser
        [HttpPost]
        [Route("Adduser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequestDto request)
        {
            //Create IdentityUser object
            var User = new IdentityUser
            {
                UserName = request.Name?.Trim(),
                Email = request.Email?.Trim()
            };
            //Create User
            var identityResult = await userManager.CreateAsync(User, request.Password);
            
            if(identityResult.Succeeded)
            {
                //Add Role to user(Reader)
                identityResult = await userManager.AddToRoleAsync(User, "Reader");

                if(identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if(identityResult.Errors.Any())
                {
                    foreach(var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }
    }
}
