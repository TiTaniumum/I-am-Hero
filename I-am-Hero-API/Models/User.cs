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
        public DateTime RegistrationDate { get; set; }
        public ICollection<Token> Tokens { get; set; } = new List<Token>();
        public Hero? Hero { get; set; }
        public ICollection<HeroSkill> HeroSkills { get; set; } = new List<HeroSkill>();
        public ICollection<HeroAttribute> HeroAttributes { get; set; } = new List<HeroAttribute>();
        public ICollection<HeroStatusEffect> HeroStatusEffects { get; set; } = new List<HeroStatusEffect>();
        public ICollection<HeroBioPiece> HeroBioPieces { get; set; } = new List<HeroBioPiece>();
        public ICollection<HeroAchievement> HeroAchievements { get; set; } = new List<HeroAchievement>();
        public ICollection<Quest> Quests { get; set; } = new List<Quest>();
        public ICollection<QuestLine> QuestsLines { get; set; } = new List<QuestLine>();
        public ICollection<QuestBehaviour> QuestBehaviours { get; set; } = new List<QuestBehaviour>();
    }
}
