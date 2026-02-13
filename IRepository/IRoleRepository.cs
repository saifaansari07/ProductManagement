using ProductWebApi.DTO;
using ProductWebApi.Models;

namespace ProductWebApi.IRepository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRoleAsync();

        Task<Role> GetRoleByIdAsync(int id);

        Task CreateRole(RoleDTO role);

        Task UpdateRole(RoleDTO role);

        Task DeleteRole(int id);
    }
}
