using System.ComponentModel.DataAnnotations;
using static TennisAcademyApp.GCommon.Validations.RequiredMessages.Ball;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Ball;

namespace TennisAcademyApp.ViewModels.Ball
{
    public class BallCreateInputModel
    {
        [Required(ErrorMessage = BrandRequiredErrorMessage)]
        public string Brand { get; set; } = null!;
        [Required(ErrorMessage = ModelRequiredErrorMessage)]
        public string Model { get; set; } = null!;
        [Required(ErrorMessage = PriceRequiredErrorMessage)]
        [Range(17.00, 80.00, ErrorMessage = PriceRangeErrorMessage)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = QuantityRequiredErrorMessage)]
        public int Quantity { get; set; }
        [Required(ErrorMessage = ImageUrlRequiredErrorMessage)]
        public string ImageUrl { get; set; } = null!;
    }
}
