using Microsoft.EntityFrameworkCore;
using ProductWebApi.Data;
using ProductWebApi.DTO;
using ProductWebApi.IRepository;
using ProductWebApi.Models;

namespace ProductWebApi.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context) {
            _context = context;
        }
        public async Task CreateRole(RoleDTO role)
        {
          
          await  _context.Roles.AddAsync(new Role
          {
              RoleName = role.RoleName,
              Description = role.Description,
          });
          await _context.SaveChangesAsync();
        }

        public async Task DeleteRole(int id)
        {
            var getById = await _context.Roles.FindAsync(id);
            if (getById != null)
            {
                _context.Roles.Remove(getById);
                await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException($"Role with id {id} not found.");
        }

        public async Task<IEnumerable<Role>> GetRoleAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if(role != null)
            {
                return role;
            }
             throw new KeyNotFoundException($"Role with id {id} not found.");
        }

        public async Task UpdateRole(RoleDTO role)
        {
             _context.Roles.Update(new Role
             {
                 RoleName = role.RoleName,
                 Description = role.Description,
             });
            await _context.SaveChangesAsync();
        }
    }
}
