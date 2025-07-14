using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TennisAcademyApp.Data.Models
{
    [Comment("Tennis Academy Coaches")]
    public class Coach
    {
        [Key]
        [Comment("Coach Identifier")]
        public Guid CoachId { get; set; } = Guid.NewGuid();
        [Required]
        [Comment("Coach Name")]
        public string Name { get; set; } = null!;
        [Comment("Coach Image")]
        public string? ImageUrl { get; set; }

        [Required]
        [Comment("Coach Age")]
        public int Age { get; set; }
        [Required]
        [Comment("Coach Description")]
        public string Description { get; set; } = null!;
        [Required]
        [Comment("Foreign key of IdentityUser")]
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public virtual ICollection<UserCoach> UsersCoaches { get; set; } = new HashSet<UserCoach>();
    }
}
