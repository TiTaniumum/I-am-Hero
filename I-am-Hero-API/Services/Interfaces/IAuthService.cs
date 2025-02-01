using I_am_Hero_API.DTO;
using I_am_Hero_API.Models;

namespace I_am_Hero_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterUser(UserDto dto);
        Task<Token?> RegenerateToken(HttpContext httpContext);
        Task<User?> GetUser(UserDto dto);
        Task<User?> GetUser(HttpContext httpContext);
        Task<User?> GetUser(string tokenHash);
        Task<Token> CreateToken(User user, string tokenHash, long ApplicationId);
        string GenerateToken(User user);
    }
}
