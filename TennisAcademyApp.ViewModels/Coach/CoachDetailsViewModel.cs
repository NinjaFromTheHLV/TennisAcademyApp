namespace TennisAcademyApp.ViewModels.Coach
{
    public class CoachDetailsViewModel
    {
        public int CoachId { get; set; }
        public string CoachName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Description { get; set; } = null!;
        public int CoachAge { get; set; }
        public string Nationality { get; set; } = null!;
        public bool IsAddedBy { get; set; }
        public bool IsInUserFavorites { get; set; }

    }
}
