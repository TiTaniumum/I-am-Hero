using System.ComponentModel.DataAnnotations;

namespace I_am_Hero_API.Models
{
    public class HeroBioPiece
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        [Required]
        public string Text { get; set; } = null!;
        public DateTime CreateDate { get; set; }
    }
}
