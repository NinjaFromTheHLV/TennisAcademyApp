using System.ComponentModel.DataAnnotations;
using TennisAcademyApp.ViewModels.DropDown;
using static TennisAcademyApp.GCommon.Validations.RequiredMessages.Reservation;

namespace TennisAcademyApp.ViewModels.Reservation
{
    public class ReservationCreateInputModel
    {
        [Required(ErrorMessage = RequiredDateMessage)]
        [Display(Name = "Select Date & Time")]
        public DateTime Date { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required(ErrorMessage = RequiredSurfaceMessage)]
        [Display(Name = "Select a surface")]
        public int SurfaceId { get; set; }

        [Required(ErrorMessage = RequiredTrainingTypeMessage)]
        [Display(Name = "Select a training type")]
        public int TrainingTypeId { get; set; }

        [Required(ErrorMessage = RequiredCoachMessage)]
        [Display(Name = "Select a coach")]
        public int CoachId { get; set; }
        public string? Note { get; set; }

        public IEnumerable<CoachDropDownModel> Coaches { get; set; } = new List<CoachDropDownModel>();
        public IEnumerable<SurfaceDropDownModel> Surfaces { get; set; } = new List<SurfaceDropDownModel>();
        public IEnumerable<TrainingTypeDropDownModel> TrainingTypes { get; set; } = new List<TrainingTypeDropDownModel>();
    }
}
