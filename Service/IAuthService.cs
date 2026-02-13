using ProductWebApi.DTO;

namespace ProductWebApi.Service
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDTO register);

        Task<AuthResponse> LoginAsync(LoginDTO login);
    }
}
