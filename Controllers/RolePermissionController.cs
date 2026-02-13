using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.DTO;
using ProductWebApi.IRepository;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        public RolePermissionController(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;   
        }

        [HttpPost]
        public async Task<IActionResult> Create(PermissionRoleDTO dto)
        {
            if (!await _rolePermissionRepository.RoleExistAsync(dto.RoleId))
                return NotFound("Role not found");
            if (!await _rolePermissionRepository.PermissionExistAsync(dto.PermissionId))
                return NotFound("Permission not found.");
            if(await _rolePermissionRepository.ExistAsync(dto.RoleId,dto.PermissionId))
                    return NotFound("Permission already assigned to role");

            var rolepermission = new RolePermission
            {
                RoleId = dto.RoleId,
                PermissionId = dto.PermissionId,
            };
            await _rolePermissionRepository.AddAsync(rolepermission);
            return Ok("Permission assigned to role");
        }

        [HttpGet("roleid")]
        public async Task<IActionResult> GetByRole(int id)
        {
            var permission  = await _rolePermissionRepository.GetRoleByIdAsync(id);
            return Ok(permission.Select(p => new
            {
                p.Id,
                p.Permission.Description
            }));
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var getById = _rolePermissionRepository.GetByIdAsync(id);
            if (getById == null)
                return NotFound("RolePermission not found.");
            await _rolePermissionRepository.RemoveAsync(id);
            return Ok("Permission remove from role");
        }
    }
}
