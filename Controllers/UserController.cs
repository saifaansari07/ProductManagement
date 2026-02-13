using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Data;
using ProductWebApi.DTO;
using ProductWebApi.IRepository;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDetails()
        {
            var data = await _userRepository.GetUsersAsync();
            return data != null ? Ok(data) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetailsById(int id)
        {
            var data = await _userRepository.GetUserById(id);
            return data != null ? Ok(data) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UsersDTO users)
        {
            var data =await  _userRepository.UpdateUser(users);
            return data != null ? Ok("User updated successfully") : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var data = await _userRepository.DeleteUser(id);
            return data!=null ? Ok("User deleted successfully"): NotFound();
        }
    }
}
