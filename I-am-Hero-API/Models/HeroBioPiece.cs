namespace I_am_Hero_API.Models
{
    public class HeroBioPiece
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public string? Text { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
