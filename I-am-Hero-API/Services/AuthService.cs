using I_am_Hero_API.Data;
using I_am_Hero_API.DTO;
using I_am_Hero_API.Models;
using I_am_Hero_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace I_am_Hero_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext context;
        private readonly PasswordHasher<object> hasher = new();

        public AuthService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<User?> RegisterUser(UserDto dto)
        {
            if (await DoUserExist(dto.Email))
            {
                return null; // Юзер существует и не может быть зарегестрирован
            }
#pragma warning disable CS8625
            string passwordHash = hasher.HashPassword(null, dto.Password);
#pragma warning restore CS8625
            User newUser = new User { Email = dto.Email, PasswordHash = passwordHash };
            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return newUser;
        }
        
        public async Task<User?> GetUser(UserDto dto)
        {
            User? user = await context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user == null)
            {
                return null;
            }
#pragma warning disable CS8625
            if (hasher.VerifyHashedPassword(null, user.PasswordHash, dto.Password) == PasswordVerificationResult.Failed)
#pragma warning restore CS8625
            {
                return null;
            }
            return user;
        }
        public async Task<User?> GetUser(string tokenHash)
        {
            Token? token = await context.Tokens.FirstOrDefaultAsync(x => x.token == tokenHash && x.ExpireDate > DateTime.Now);
            return token?.User;
        }
        public async Task<Token> CreateToken(User user, string tokenHash, long ApplicationId)
        {
            Token token = new Token { token = tokenHash, ApplicationId = ApplicationId };
            user.Tokens.Add(token);
            await context.SaveChangesAsync();
            return token;
        }
        private Token? GetToken(User? user, long ApplicationId)
        {
            return user?.Tokens.FirstOrDefault(x => x.ExpireDate > DateTime.Now && x.ApplicationId == ApplicationId);
        }

        private async Task<bool> DoUserExist(string Email)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Email == Email) != null;
        }
    }
}
