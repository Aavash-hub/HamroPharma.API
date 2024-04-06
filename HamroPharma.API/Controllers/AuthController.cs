using HamroPharma.API.Models.DTO;
using HamroPharma.API.Repositories.Interface;
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
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //POST: {apibaseurl}/api/auth/login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
           var identityUser = await userManager.FindByEmailAsync(request.Email);
            if (identityUser is not null)
            {
                var CheckpasswordResults = await userManager.CheckPasswordAsync(identityUser, request.password);

                if (CheckpasswordResults)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);
                    //Create a Token and Response
                    var JwtToken = tokenRepository.createJwtToken(identityUser, roles.ToList());

                    var response = new LoginResponseDto()
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = JwtToken

                    };
                    return Ok(response);
                }
            }
            ModelState.AddModelError("","Email or Password is incorrect");

            return ValidationProblem(ModelState);
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
                Email = request.Email?.Trim(),
                PhoneNumber = request.Number?.Trim()
            };
            //Create User
            var identityResult = await userManager.CreateAsync(User, request.password);
            
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
