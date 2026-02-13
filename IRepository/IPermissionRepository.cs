using ProductWebApi.Models;

namespace ProductWebApi.IRepository
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetPermissionList();

        Task<Permission> GetPermissionById(int id);

        Task Create(Permission permission);

        Task Update(Permission permission);
        Task Delete(int id);
    }
}
