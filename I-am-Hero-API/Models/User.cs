using System.ComponentModel.DataAnnotations;

namespace I_am_Hero_API.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public List<Token> Token { get; set; }
    }
}
