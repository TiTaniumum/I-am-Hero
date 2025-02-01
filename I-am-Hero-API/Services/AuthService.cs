using Humanizer;
using I_am_Hero_API.Data;
using I_am_Hero_API.DTO;
using I_am_Hero_API.Models;
using I_am_Hero_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Versioning;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace I_am_Hero_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext context;
        private readonly PasswordHasher<object> hasher = new();
        private readonly IConfiguration configuration;
        private readonly int applicationsCount;
        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
            applicationsCount = context.Applications.Count();
        }
        public async Task<Token?> RegenerateToken(HttpContext httpContext)
        {
            Token? token = await GetToken(httpContext);
            if (token == null || token.CreateDate.AddDays(1) > DateTime.Now)
                return null;
            User? user = await GetUser(httpContext);
            if (user == null) 
                return null;
            string tokenHash = GenerateToken(user);
            token.token = tokenHash;
            token.CreateDate = DateTime.Now;
            await context.SaveChangesAsync();
            return token;
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
        public async Task<Token?> Login(AuthDto dto) 
        {
            if (dto.ApplicationId <= 0 || dto.ApplicationId > applicationsCount)
                throw new ArgumentOutOfRangeException("ApplicationId is out of range");
            User? user = await GetUser(new UserDto(dto));
            if(user == null) return null;
            Token? existingToken = await context.Tokens.FirstOrDefaultAsync(x => x.UserId == user.Id && x.ApplicationId == dto.ApplicationId);
            if (existingToken != null && existingToken.CreateDate.AddDays(1) > DateTime.Now) {
                return existingToken;
            }
            string tokenHash = GenerateToken(user);
            Token newToken = await CreateToken(user, tokenHash, dto.ApplicationId, existingToken);
            return newToken;
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
        public async Task<User?> GetUser(HttpContext httpContext)
        {
            string? userEmail = httpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            User? user = await context.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
            return user;
        }
        public async Task<Token> CreateToken(User user, string tokenHash, long ApplicationId, Token? existingToken = null)
        {
            if (existingToken != null)
            {
                existingToken.token = tokenHash;
                existingToken.CreateDate = DateTime.Now;
                return existingToken;
            }
            Token newToken = new Token { token = tokenHash, ApplicationId = ApplicationId };
            user.Tokens.Add(newToken);
            await context.SaveChangesAsync();
            return newToken;
        }

        public string GenerateToken(User user)
        {
            var jwtKey = configuration["Jwt:Key"];
            var jwtIssuer = configuration["Jwt:Issuer"];
            var jwtAudience = configuration["Jwt:Audience"];
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(14),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<Token?> GetToken(HttpContext httpContext)
        {
            string authStr = httpContext.Request.Headers.Authorization.ToString();
            string tokenHash = authStr.Split(" ")[1];
            Token? token = await context.Tokens.FirstOrDefaultAsync(x => x.token == tokenHash);
            return token;
        }
        private async Task<bool> DoUserExist(string Email)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Email == Email) != null;
        }
    }
}
