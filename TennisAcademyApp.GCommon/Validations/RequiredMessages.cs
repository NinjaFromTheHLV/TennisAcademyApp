namespace TennisAcademyApp.GCommon.Validations
{
    public static class RequiredMessages
    {
        public static class Coach
        {
            public const string RequiredNameMessage = "Name is required.";
            public const string DescriptionRequiredMessage = "Description is required.";
            public const string AgeRequiredMessage = "Age is required.";
        }
        public static class Reservation
        {
            public const string RequiredDateMessage = "Please select Date & Time.";
            public const string RequiredCoachMessage = "Please select a coach.";
            public const string RequiredSurfaceMessage = "Please select a surface.";
            public const string RequiredTrainingTypeMessage = "Please select a training type.";
        }
        public static class Racket 
        {
            public const string BrandRequiredErrorMessage = "Brand is required.";
            public const string ModelRequiredErrorMessage = "Model is required.";
            public const string PriceRequiredErrorMessage = "Price is required.";
            public const string QuantityRequiredErrorMessage = "Quantity is required.";
            public const string ImageUrlRequiredErrorMessage = "Image URL is required.";
            public const string RacketNotFoundErrorMessage = "Racket not found.";
        }
        public static class Ball
        {
            public const string BrandRequiredErrorMessage = "Brand is required.";
            public const string ModelRequiredErrorMessage = "Model is required.";
            public const string PriceRequiredErrorMessage = "Price is required.";
            public const string QuantityRequiredErrorMessage = "Quantity is required.";
            public const string ImageUrlRequiredErrorMessage = "Image URL is required.";
            public const string BallNotFoundErrorMessage = "Ball not found.";
        }
        public static class Bag
        {
            public const string BrandRequiredErrorMessage = "Brand is required.";
            public const string ModelRequiredErrorMessage = "Model is required.";
            public const string PriceRequiredErrorMessage = "Price is required.";
            public const string QuantityRequiredErrorMessage = "Quantity is required.";
            public const string ImageUrlRequiredErrorMessage = "Image URL is required.";
        }
    }
}
