namespace TennisAcademyApp.GCommon.Validations
{
    public static class ErrorMessages
    {
        public const string InvalidData = "Invalid data, please try again";
        public const string UnexpectedError = "An unexpected error occurred, please try again later.";
        public static class Reservation
        {
            public const string PastDateErrorMessage = "Please select a valid date.";
            public const string TwoHoursErrorMessage = "Reservations can be made at least two hours from now.";    
            public const string SelectedTimeErrorMessage = "The Academy's work time is from 8:00 to 20:00.";
            public const string FutureDateErrorMessage = "Reservations can be made for the next 14 days.";
            public const string SundayErrorMessage = "Sunday is off day! Please choose other time.";

            public const string DurationErrorMessage = "Duration must be either 60 or 120 minutes.";

            public const string CoachNotAvailableErrorMessage = "The selected coach is not available at the chosen time.";

            public const string ReservationNotFoundErrorMessage = "Reservation not found.";

            public const string ReservationDeleteErrorMessage = "An error occurred while deleting the reservation, try again.";
        }
        public static class Coach
        {
            public const string AgeErrorMessage = "Age must be between 30 and 75!";

            public const string DescriptionMinLengthMessage = "Description must be at least 10 characters.";
            public const string DescriptionMaxLengthMessage = "Description cannot exceed 150 characters.";

            public const string CoachNotFoundErrorMessage = "Coach not found."; 
            public const string CoachCannotBeNullErrorMessage = "Coach cannot be null.";

            public const string CoachAddErrorMessage = "An error occured while adding a coach, try again.";
            public const string CoachEditErrorMessage = "An error occured while editing a coach, try again.";
            public const string CoachDeleteErrorMessage = "An error occured while deleting a coach, try again.";

            public const string CoachAlreadyAddedToFavouritesErrorMessage = "Coach already added to favourites.";
            
        }
        public static class Racket
        {
            public const string RacketNotFoundErrorMessage = "Racket not found.";
            public const string RacketCannotBeNullErrorMessage = "Racket cannot be null.";

            public const string PriceRangeErrorMessage = "Price must be between 30 and 1500.";
            public const string QuantityRangeErrorMessage = "Quantity must be a positive number.";

            public const string RacketAddErrorMessage = "An error occurred while adding the racket, try again.";
            public const string RacketEditErrorMessage = "An error occurred while editing the racket, try again.";
            public const string RacketDeleteErrorMessage = "An error occurred while deleting the racket, try again.";
        }
        public static class User
        {
            public const string UserCannotBeNull = "Please log in and try again.";
        }
        public static class RacketCart
        {
            public const string InvalidQuantityErrorMessage = "Invalid quantity. Please enter a valid number.";
            public const string RacketFailedToRemoveFromCartErrorMessage = "An error occurred while removing the racket from the cart, try again.";
            public const string UnableToCheckoutErrorMessage = "You cannot checkout an empty cart. Please add a racket to the cart first.";
            public const string CannotLoadRacketCartErrorMessage = "An error occurred while loading the racket cart, try again.";
            public const string RacketNotFoundInCartErrorMessage = "Racket not found in cart.";
        }
        public static class Ball
        {
            public const string BallNotFoundErrorMessage = "Ball not found.";
            public const string BallCannotBeNullErrorMessage = "Ball cannot be null.";

            public const string PriceRangeErrorMessage = "Price must be between 17 and 80.";
            public const string QuantityRangeErrorMessage = "Quantity must be a positive number.";

            public const string BallAddErrorMessage = "An error occurred while adding the ball, try again.";
            public const string BallEditErrorMessage = "An error occurred while editing the ball, try again.";
            public const string BallDeleteErrorMessage = "An error occurred while deleting the ball, try again.";
        }
        public static class BallCart
        {
            public const string InvalidQuantityErrorMessage = "Invalid quantity. Please enter a valid number.";
            public const string BallFailedToRemoveFromCartErrorMessage = "An error occurred while removing the ball from the cart, try again.";
            public const string UnableToCheckoutErrorMessage = "You cannot checkout an empty cart. Please add a ball to the cart first.";
            public const string CannotLoadBallCartErrorMessage = "An error occurred while loading the ball cart, try again.";
            public const string BallNotFoundInCartErrorMessage = "Ball not found in cart.";
        }
        public static class Bag
        {
            public const string PriceRangeErrorMessage = "Price must be between 50.00 and 1000.00.";
            public const string QuantityErrorMessage = "Quality must be a positive number";

            public const string BagNotFoundErrorMessage = "Bag not found.";
            public const string BagCannotBeNullErrorMessage = "Bag cannot be null.";

            public const string BagAddErrorMessage = "An error occurred while adding the bag, try again.";
            public const string BagEditErrorMessage = "An error occurred while editing the bag, try again.";
            public const string BagDeleteErrorMessage = "An error occurred while deleting the bag, try again.";
        }
        public static class BagCart
        {
            public const string InvalidQuantityErrorMessage = "Invalid quantity. Please enter a valid number.";
            public const string BagFailedToRemoveFromCartErrorMessage = "An error occurred while removing the bag from the cart, try again.";
            public const string UnableToCheckoutErrorMessage = "You cannot checkout an empty cart. Please add a bag to the cart first.";
            public const string CannotLoadBagCartErrorMessage = "An error occurred while loading the bag cart, try again.";
            public const string BagNotFoundInCartErrorMessage = "Bag not found in cart.";
        }
    }
}
