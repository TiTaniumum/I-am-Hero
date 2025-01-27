using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using I_am_Hero_API.Models;
using I_am_Hero_API.DTO;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using I_am_Hero_API.Data;

namespace I_am_Hero_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegistrationDto userRegistrationDto)
        {
            User? user = await _context.Users
                    .Where(x => x.Email == userRegistrationDto.Email)
                    .FirstOrDefaultAsync();
            if (user == null)
            {
                _context.Users.Add(new User { Email = userRegistrationDto.Email, PasswordHash = userRegistrationDto.Password });
                await _context.SaveChangesAsync();
                return Ok("Registered");
            }
            return Conflict(new
            {
                error = "UserAlreadyExists",
                message = "A user with this email already exists. Please try logging in instead."
            }
            );
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserRegistrationDto userRegistrationDto)
        {
            User? user = await _context.Users
                    .Where(x => x.Email == userRegistrationDto.Email && x.PasswordHash == userRegistrationDto.Password)
                    .FirstOrDefaultAsync();
            if (user != null)
            {
                Token? token = user.Tokens.Where(x => x.ExpireDate>DateTime.Now).FirstOrDefault();

                string computedTokenHash = ComputeSha256Hash(user.Email+new DateTime().GetHashCode());
                DateTime now = DateTime.Now;

                if (token != null)
                {
                    token.CreateDate = now;
                    token.token = computedTokenHash;
                }
                else
                {
                    Token newToken = new Token { token = computedTokenHash, CreateDate = now };
                    user.Tokens.Add(newToken);
                }
                await _context.SaveChangesAsync();
                return Ok(computedTokenHash);
            }
            return NotFound("couldn't find such user or foulty password");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256 object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash as a byte array
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert the byte array to a string
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert to hex format
                }

                return builder.ToString();
            }
        }
    }
}
