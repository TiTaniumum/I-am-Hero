using I_am_Hero_API.Models;

namespace I_am_Hero_API.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
