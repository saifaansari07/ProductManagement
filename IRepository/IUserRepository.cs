using ProductWebApi.DTO;
using ProductWebApi.Models;

namespace ProductWebApi.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetUsersAsync();

        Task<Users> GetUserById(int id);

        Task<bool> UpdateUser(UsersDTO users);

        Task<Users> DeleteUser(int id);
    }
}
