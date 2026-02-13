using Microsoft.EntityFrameworkCore;
using ProductWebApi.Data;

namespace ProductWebApi.Service
{
    public class PermissionService:IPermissionService
    {
        private readonly DataContext _context;

        public PermissionService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> HasPermission(string role, string permission)
        {
            return await _context.RolePermissions
                .AnyAsync(rp => rp.Role.RoleName == role && rp.Permission.Description == permission);
        }
    }
}
