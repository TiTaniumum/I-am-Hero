using System.ComponentModel.DataAnnotations;

namespace I_am_Hero_API.Models
{
    public class Application
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
