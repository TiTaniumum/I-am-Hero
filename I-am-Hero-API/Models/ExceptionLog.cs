namespace I_am_Hero_API.Models
{
    public class ExceptionLog
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public User? User { get; set; }
        public string Url { get; set; } = null!;
        public string ExceptionMessage { get; set; } = null!;
        public string? StackTrace { get; set; }
        public DateTime ExceptionDateTime { get; set; }
    }
}
