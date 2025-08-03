using System.ComponentModel.DataAnnotations;
using static TennisAcademyApp.GCommon.Validations.RequiredMessages.Bag;
using static TennisAcademyApp.GCommon.Validations.ErrorMessages.Bag;

namespace TennisAcademyApp.ViewModels.Bag
{
    public class BagCreateInputModel
    {
        [Required(ErrorMessage = BrandRequiredErrorMessage)]
        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = ModelRequiredErrorMessage)]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = PriceRequiredErrorMessage)]
        [Range(50.00, 1000.00, ErrorMessage = PriceRangeErrorMessage)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = QuantityRequiredErrorMessage)]
        [Range(1, int.MaxValue, ErrorMessage = QuantityErrorMessage)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = ImageUrlRequiredErrorMessage)]
        public string ImageUrl { get; set; } = null!;
    }
}
