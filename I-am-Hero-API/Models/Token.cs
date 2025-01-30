using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace I_am_Hero_API.Models
{
    public class Token
    {
        public long Id { get; set; }
        [Column("Token")]
        [Required]
        public string token { get; set; } = null!;
        [Comment("Дата создания токена. Дефолт = GETDATE()")]
        public DateTime CreateDate { get; set; }
        [Comment("Дата прекращения работы токена. Это вычисляемое значение от CreateDate. К нему прибавляется 14 дней.")]
        public DateTime ExpireDate { get; private set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public long ApplicationId { get; set; }
        public Application Application { get; set; } = null!;
    }
}
