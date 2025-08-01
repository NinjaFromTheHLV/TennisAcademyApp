using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TennisAcademyApp.Data.Models
{
    [Comment("Racket Cart")]
    public class RacketCart
    {
        [Key]
        [Comment("Racket Cart Identifier")]
        public int Id { get; set; }
        [Required]
        [Comment("Foreign Key of Racket")]
        public int RacketId { get; set; }
        public virtual Racket Racket { get; set; } = null!;
        [Required]
        [Comment("Foreign Key of IdentityUser")]
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;
        [Required]
        [Comment("Quantity of Rackets in Cart")]
        public int Quantity { get; set; }
    }
}
