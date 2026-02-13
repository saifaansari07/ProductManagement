using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductWebApi.Data;
using ProductWebApi.DTO;
using ProductWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductWebApi.Service
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(DataContext context, IConfiguration configuration) { 
            _context = context;
            _configuration = configuration;
        }

        public async Task RegisterAsync(RegisterDTO register)
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                if (await _context.Users.AnyAsync(u => u.UserEmail == register.UserEmail))
                    throw new Exception("Email already exist.");

                var user = new Users
                {
                    UserName = register.UserEmail,
                    UserEmail = register.UserEmail,
                    IsActive = 1
                };
                var hasher =  new PasswordHasher<Users>();
                string hashedPassword = hasher.HashPassword(user, register.Password);
                user.Password = hashedPassword;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var rolesToAssign = new List<string> {"Users",register.AccountType=="Customer"? "Customer" : "ProductSeller"};
                var roles = await _context.Roles.Where(r=>rolesToAssign.Contains(r.RoleName)).ToListAsync();
                foreach (var role in roles)
                {
                    _context.UserRoles.Add( new UserRole
                    {
                        UserId = user.UserId,
                        RoleId = role.RoleId
                    });
                }
                await _context.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
        public async Task<AuthResponse> LoginAsync(LoginDTO login)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.UserEmail == login.UserEmail);

                if (user == null)
                    throw new Exception("Invalid credentials");

                var hasher = new PasswordHasher<Users>();
                var verifyPassword = hasher.VerifyHashedPassword(user, user.Password, login.Password);

                if (verifyPassword == PasswordVerificationResult.Failed)
                    throw new Exception("Invalid username and password");

                var role =  user.UserRoles.Select(ur => ur.Role.RoleName).ToList();

                var token = GenerateToken(user, role);

                return new AuthResponse
                {
                    Token = token,
                    Username = user.UserEmail,
                    Roles = role
                };
            }
            catch(Exception ex) 
            {
                throw new Exception (ex.Message);
            }
        }

        private string GenerateToken(Users user, List<string> role)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Email,user.UserEmail)
            };
            foreach (var r in role)
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
            }

            var token = new JwtSecurityToken(
                _configuration["JwtConfig:Issuer"],
                _configuration["JwtConfig:Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.Now.AddMinutes(14),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
