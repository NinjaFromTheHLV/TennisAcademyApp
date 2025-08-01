using System.ComponentModel.DataAnnotations;
using static TennisAcademyApp.GCommon.Validations.RequiredMessages.Racket;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Racket;

namespace TennisAcademyApp.ViewModels.Racket
{
    public class RacketCreateInputModel
    {
        [Required(ErrorMessage = BrandRequiredErrorMessage)]
        public string Brand { get; set; } = null!;
        [Required(ErrorMessage = ModelRequiredErrorMessage)]
        public string Model { get; set; } = null!;
        [Required(ErrorMessage = PriceRequiredErrorMessage)]
        [Range(30.00, 1500.00, ErrorMessage = PriceRangeErrorMessage)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = QuantityRequiredErrorMessage)]
        public int Quantity { get; set; }
        [Required(ErrorMessage = ImageUrlRequiredErrorMessage)]
        public string ImageUrl { get; set; } = null!;
    }
}
