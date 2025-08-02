using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TennisAcademyApp.Data.Models
{
    [Comment("Balls Shop")]
    public class Ball
    {
        [Key]
        [Comment("Ball Identifier")]
        public int Id { get; set; }
        [Required]
        [Comment("Ball Brand")]
        public string Brand { get; set; } = null!;
        [Required]
        [Comment("Ball Model")]
        public string Model { get; set; } = null!;
        [Required]
        [Comment("Ball Price")]
        public decimal Price { get; set; }
        [Required]
        [Comment("Available in stock")]
        public int Quantity { get; set; }
        [Required]
        [Comment("Racket Image")]
        public string ImageUrl { get; set; } = null!;
    }
}
