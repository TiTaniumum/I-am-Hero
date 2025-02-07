using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class HeroBioPieceDto : TokenDto
    {
        public long? Id { get; set; }
        public string? Text { get; set; }
        public DateTime? CreateDate { get; set; }
        public HeroBioPieceDto() { }
        public HeroBioPieceDto(HeroBioPiece heroBioPiece)
        {
            Id = heroBioPiece.Id;
            Text = heroBioPiece.Text;
            CreateDate = heroBioPiece.CreateDate;
        }
    }
}
