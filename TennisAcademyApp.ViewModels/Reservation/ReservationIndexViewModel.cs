namespace TennisAcademyApp.ViewModels.Reservation
{
    public class ReservationIndexViewModel
    {
        public int ReservationId { get; set; }
        public string CoachName { get; set; } = null!;
        public string TrainingTypeName { get; set; } = null!;
        public string SurfaceName { get; set; } = null!;
        public string Date { get; set; } = null!;
        public int Duration { get; set; }
    }
}
