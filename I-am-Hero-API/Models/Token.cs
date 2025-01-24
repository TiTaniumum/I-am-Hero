using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace I_am_Hero_API.Models
{
    public class Token
    {
        public int Id { get; set; }
        [Column("Token")]
        [Required]
        public string token { get; set; }
        public User User { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }
    }
}
