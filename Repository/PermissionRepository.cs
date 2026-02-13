using Microsoft.EntityFrameworkCore;
using ProductWebApi.Data;
using ProductWebApi.IRepository;
using ProductWebApi.Models;

namespace ProductWebApi.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly DataContext _context;
        public PermissionRepository(DataContext context) {
            _context = context; 
        }
        public async Task Create(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
           var getById = await _context.Permissions.FindAsync(id);
            if (getById != null)
            {
                 _context.Permissions.Remove(getById);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<Permission> GetPermissionById(int id)
        {
            var getById = await _context.Permissions.FindAsync(id);
            if (getById != null) { 
                return getById;
            }
            throw new KeyNotFoundException($"Permission with id {id} is not found");
        }

        public async Task Update(Permission permission)
        {
             _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Permission>> GetPermissionList()
        {
            return await _context.Permissions.ToListAsync();
        }
    }
}
