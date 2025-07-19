namespace TennisAcademyApp.ViewModels.Coach
{
    public class FavouriteCoachViewModel
    {
        public int CoachId { get; set; }
        public string CoachName { get; set; } = null!;
        public int CoachAge { get; set; }
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
