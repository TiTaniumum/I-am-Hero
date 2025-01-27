using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace I_am_Hero_API.Models
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;
        [Comment("Подтверждение пользователем свой почты.")]
        public bool IsEmailVerified { get; set; }
        public ICollection<Token> Tokens { get; set; } = new List<Token>();
        public Hero Hero { get; set; }
        public ICollection<HeroSkill> HeroSkills { get; set; }
    }
}
