using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TennisAcademyApp.Data.Models
{
    public class BagCart
    {
        [Required]
        [Comment("Foreign Key of Bag")]
        public int BagId { get; set; }
        public virtual Bag Bag { get; set; } = null!;

        [Required]
        [Comment("Foreign Key of IdentityUser")]
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;

        [Required]
        [Comment("Quantity of Bags in Cart")]
        public int Quantity { get; set; }
    }
}
