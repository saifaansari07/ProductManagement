using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.DTO;
using ProductWebApi.IRepository;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository) {
        _roleRepository = roleRepository;   
        }
        [HttpGet]
        public async Task<IActionResult> GetRole()
        {
            var result = await _roleRepository.GetRoleAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Record not Found");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var result = await _roleRepository.GetRoleByIdAsync(id);
            if (result != null) { 
                return Ok(result);
            }
            return NotFound($"Record with id {id} is not found");
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleDTO role)
        {
            await _roleRepository.CreateRole(role);
            return Ok("Role created successfully");
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(RoleDTO role)
        {
            await _roleRepository.UpdateRole(role);
            return Ok("Role Updated successfully");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var getById =await _roleRepository.GetRoleByIdAsync(id);
            if (getById != null)
            {
                await _roleRepository.DeleteRole(id);
                return Ok("Role deleted successfully");
            }
            return NotFound($"Record with id {id} is not found");
        }

    }
}
