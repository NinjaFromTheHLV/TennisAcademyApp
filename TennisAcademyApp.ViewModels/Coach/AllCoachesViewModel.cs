namespace TennisAcademyApp.ViewModels.Coach
{
    public class AllCoachesViewModel
    {
        public Guid CoachId { get; set; }
        public string CoachName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int CoachAge { get; set; }
        public string Description { get; set; } = null!;
        //public bool IsInUserFavourites { get; set; }
    }
}
