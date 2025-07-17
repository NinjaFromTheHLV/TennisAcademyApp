using System.ComponentModel.DataAnnotations;
using static TennisAcademyApp.ViewModels.Validations.CoachInputModelValidations;
using static TennisAcademyApp.GCommon.ValidationConstants.Coach;

namespace TennisAcademyApp.ViewModels.Coach
{
    public class AddCoachInputModel
    {
        [Required(ErrorMessage = RequiredNameMessage)]
        [Display(Name = "Coach Name")]
        [MinLength(CoachNameMinLenght, ErrorMessage = NameMinLengthMessage)]
        [MaxLength(CoachNameMaxLenght, ErrorMessage = NameMaxLengthMessage)]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = AgeRequiredMessage)]
        [Display(Name = "Coach Age")]
        [MinLength(CoachAgeMinLenght, ErrorMessage = AgeMinLengthMessage)]
        [MaxLength(CoachAgeMaxLenght, ErrorMessage = AgeMaxLengthMessage)]
        public int Age { get; set; }

        [Required(ErrorMessage = DescriptionRequiredMessage)]
        [Display(Name = "Coach Description")]
        [MinLength(CoachDescriptionMinLenght, ErrorMessage = DescriptionMinLengthMessage)]
        [MaxLength(CoachDescriptionMaxLenght, ErrorMessage = DescriptionMaxLengthMessage)]
        public string Description { get; set; } = null!;
        [Display(Name = "Coach Image")]
        public string? ImageUrl { get; set; }

    }
}
