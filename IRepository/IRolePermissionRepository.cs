using ProductWebApi.Models;

namespace ProductWebApi.IRepository
{
    public interface IRolePermissionRepository
    {
        Task<bool> ExistAsync(int roleid, int permissionid);

        Task AddAsync(RolePermission permission);

        Task RemoveAsync(int rolepermissionid);

        Task<List<RolePermission>> GetRoleByIdAsync(int id);

        Task<RolePermission> GetByIdAsync(int id);

        Task<bool> RoleExistAsync (int roleid); 

        Task<bool> PermissionExistAsync(int permissionid);
    }
}
