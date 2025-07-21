using System.ComponentModel.DataAnnotations;

namespace TennisAcademyApp.ViewModels.Coach
{
    public class DeleteCoachViewModel
    {
        [Required(ErrorMessage = "CoachId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid coach ID.")]
        public int CoachId { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }

    }
}
