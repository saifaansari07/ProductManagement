using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.IRepository;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPermission()
        {
            var result = await _permissionRepository.GetPermissionList();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Record not Found");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            var result = await _permissionRepository.GetPermissionById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"Record with id {id} is not found");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Permission permission)
        {
            await _permissionRepository.Create(permission);
            return Ok("Permission created successfully");
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(Permission permission)
        {
            await _permissionRepository.Update(permission);
            return Ok("Permission Updated successfully");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var getById = await _permissionRepository.GetPermissionById(id);
            if (getById != null)
            {
                await _permissionRepository.Delete(id);
                return Ok("Permission deleted successfully");
            }
            return NotFound($"Record with id {id} is not found");
        }
    }
}
