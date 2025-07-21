using System.ComponentModel.DataAnnotations;
using static TennisAcademyApp.ViewModels.Validations.CoachInputModelValidations;
using static TennisAcademyApp.GCommon.Validations.ValidationConstants.Coach;

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
        [Range(CoachAgeMinLenght, CoachAgeMaxLenght, ErrorMessage = AgeErrorMessage)]
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
