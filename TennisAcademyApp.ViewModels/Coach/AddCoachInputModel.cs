using System.ComponentModel.DataAnnotations;
using static TennisAcademyApp.GCommon.Validations.RequiredMessages.Coach;
using static TennisAcademyApp.GCommon.Validations.ValidationConstants.Coach;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Coach;

namespace TennisAcademyApp.ViewModels.Coach
{
    public class AddCoachInputModel
    {
        [Required(ErrorMessage = RequiredNameMessage)]
        [Display(Name = "Coach Name")]
        [MaxLength(CoachNameMaxLenght, ErrorMessage = NameMaxLengthMessage)]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = AgeRequiredMessage)]
        [Display(Name = "Coach Age")]
        [Range(CoachAgeMinRequirement, CoachAgeMaxRequirement, ErrorMessage = AgeErrorMessage)]
        public int Age { get; set; }

        [Required(ErrorMessage = DescriptionRequiredMessage)]
        [Display(Name = "Coach Description")]
        [MinLength(CoachDescriptionMinLenght, ErrorMessage = DescriptionMinLengthMessage)]
        [MaxLength(CoachDescriptionMaxLenght, ErrorMessage = DescriptionMaxLengthMessage)]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Nationality is required!")]
        [Display(Name = "Coach Nationality")]
        public string Nationality { get; set; } = null!;
        [Display(Name = "Coach Image")]
        public string? ImageUrl { get; set; }

    }
}
