using System.ComponentModel.DataAnnotations;

namespace I_am_Hero_API.Models
{
    public class cLevelCalculationType
    {
        public long Id { get; set; }
        [Required]
        public string NameRu { get; set; } = null!;
        [Required]
        public string NameEn { get; set; } = null!;
    }
}
