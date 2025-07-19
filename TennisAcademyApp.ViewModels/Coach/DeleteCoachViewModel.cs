using System.ComponentModel.DataAnnotations;

namespace TennisAcademyApp.ViewModels.Coach
{
    public class DeleteCoachViewModel
    {
        public int CoachId { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }

    }
}
