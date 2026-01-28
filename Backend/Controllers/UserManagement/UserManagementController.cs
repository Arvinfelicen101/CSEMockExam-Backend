using Backend.DTOs.UserManagement;
using Backend.Repository.UserManagement;
using Backend.Services.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace Backend.Controllers.UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementServices _service;

       public UserManagementController(IUserManagementServices service) { 
        
            _service = service;

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUser([FromBody] UserManagementCreateDTO dto)
        {
            await _service.CreateUserAsync(dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
           var users = await _service.GetUsersAsync();
            return Ok(users);
        }

        [HttpPut("({id:Guid})")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid id, UserManagementUpdateDTO dto)
        {
            await _service.UpdateUserAsync(id.ToString(), dto);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("({id:Guid})")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _service.DeleteUserAsync(id.ToString());
            return NoContent();
        }
    }
}
