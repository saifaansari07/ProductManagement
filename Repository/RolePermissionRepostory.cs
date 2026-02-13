using Microsoft.EntityFrameworkCore;
using ProductWebApi.Data;
using ProductWebApi.IRepository;
using ProductWebApi.Models;

namespace ProductWebApi.Repository
{
    public class RolePermissionRepostory : IRolePermissionRepository
    {
        private readonly DataContext _context;
        public RolePermissionRepostory(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(RolePermission permission)
        {
            _context.RolePermissions.Add(permission);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(int roleid, int permissionid)
        {
          return await _context.RolePermissions.AnyAsync(rp =>rp.RoleId==roleid && rp.PermissionId==permissionid);
        }

        public async Task<RolePermission> GetByIdAsync(int id)
        {
          return  await _context.RolePermissions.Include(rp=>rp.Permission)
                .Include(rp=>rp.Role).FirstOrDefaultAsync(rp=>rp.Id==id);
        }

        public Task<List<RolePermission>> GetRoleByIdAsync(int id)
        {
            return _context.RolePermissions
                .Include(rp=>rp.Permission)
                .Where(r=>r.RoleId==id)
                .ToListAsync();
        }

        public async Task<bool> PermissionExistAsync(int permissionid)
        {
            return await _context.Permissions.AnyAsync(p=>p.PermissionId==permissionid);
        }

        public async Task RemoveAsync(int rolepermissionid)
        {
            var getById = await _context.RolePermissions.FindAsync(rolepermissionid);
            if(getById==null)   
                   throw new KeyNotFoundException("RolePermission not found");
            _context.RolePermissions.Remove(getById);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RoleExistAsync(int roleid)
        {
            return await _context.Roles.AnyAsync(r=>r.RoleId == roleid);
        }
    }
}
