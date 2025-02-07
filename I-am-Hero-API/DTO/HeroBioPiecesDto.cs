using I_am_Hero_API.Models;

namespace I_am_Hero_API.DTO
{
    public class HeroBioPiecesDto : TokenDto
    {
        public IEnumerable<HeroBioPieceDto> HeroBioPieces { get; set; } = null!;
        public HeroBioPiecesDto() { }
        public HeroBioPiecesDto(IEnumerable<HeroBioPieceDto> heroBioPieces)
        {
            HeroBioPieces = heroBioPieces;
        }
        public HeroBioPiecesDto(IEnumerable<HeroBioPiece> heroBioPieces)
        {
            HeroBioPieces = heroBioPieces.Select(x => new HeroBioPieceDto(x));
        }
    }
}
