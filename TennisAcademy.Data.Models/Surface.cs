using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TennisAcademyApp.Data.Models
{
    [Comment("Tennis Academy Surfaces")]
    public class Surface
    {
        [Key]
        [Comment("Surface Identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Surface Name")]
        public string Name { get; set; } = null!;
        [Comment("Ïmage of the surface")]
        public string ImageUrl { get; set; } = null!;
        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    }
}
