namespace TennisAcademyApp.ViewModels.Reservation
{
    public class ReservationHistoryViewModel
    {
        public int ReservationId { get; set; }
        public string CoachName { get; set; } = null!;
        public string TrainingTypeName { get; set; } = null!;
        public string SurfaceImageUrl { get; set; } = null!;
        public string SurfaceName { get; set; } = null!;
    }
}
