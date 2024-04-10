﻿using HamroPharma.API.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HamroPharma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        //get:https://localhost:7144/api/User/getalluser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserListDto>>> GetAllUsers()
        {
            var users = await _userManager.Users
                .Select(u => new UserListDto
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                })
                .ToListAsync();

            return Ok(users);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
        }
    }
}
