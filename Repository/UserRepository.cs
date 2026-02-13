using Microsoft.EntityFrameworkCore;
using ProductWebApi.Data;
using ProductWebApi.DTO;
using ProductWebApi.IRepository;
using ProductWebApi.Models;
using System.Security.Claims;

namespace ProductWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public UserRepository(DataContext context,IHttpContextAccessor httpContext)
        {
            _context=context;
            _httpContext=httpContext;
        }
        public async Task<Users> DeleteUser(int id)
        {
            var getById = await _context.Users.FindAsync(id);
             _context.Users.Remove(getById);
            await _context.SaveChangesAsync();
            return getById;
        }

        public async Task<Users> GetUserById(int id)
        {
            var user = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == "Admin")
                return await _context.Users.FindAsync(id);
            if(int.TryParse(user,out int loggedin) && loggedin==id)
               return await _context.Users.FindAsync(id);

            return null;
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            var user = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if(role=="Admin")
               return await  _context.Users.ToListAsync();
            var userid = await _context.Users.Where(p => p.UserId.ToString()== user).ToListAsync();
            return userid!=null ? userid :null;
        }

        public async Task<bool> UpdateUser(UsersDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dto.UserId);
            if (user == null)
                return false;
            user.UserName = dto.UserName;
            user.UserEmail = dto.UserEmail;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
