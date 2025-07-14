using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TennisAcademyApp.Data.Models
{
    [Comment("Users Favourite Coach")]
    public class UserCoach
    {
        [Comment("Foreign Key which references to IdentityUser")]
        [Required]
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;
        [Comment("Foreign Key which references to IdentityUser")]
        [Required]
        public Guid CoachId { get; set; }
        public virtual Coach Coach { get; set; } = null!;
    }
}
