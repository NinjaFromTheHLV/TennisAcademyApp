namespace TennisAcademyApp.GCommon.Validations
{
    public static class ErrorMessages
    {
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
            public const string YouCannotSeeOthersReservationsErrorMessage = "You cannot see other users' reservations.";
        }
        public static class Coach
        {
            public const string AgeErrorMessage = "Age must be between 30 and 75!";

            public const string DescriptionMinLengthMessage = "Description must be at least 10 characters.";
            public const string DescriptionMaxLengthMessage = "Description cannot exceed 150 characters.";

            public const string CoachNotFoundErrorMessage = "Coach not found."; 
        }
        public static class Racket
        {
            public const string RacketNotFoundErrorMessage = "Racket not found.";
            public const string RacketCannotBeNullErrorMessage = "Racket cannot be null.";

            public const string PriceRangeErrorMessage = "Price must be between 30 and 1500.";
            public const string QuantityRangeErrorMessage = "Quantity must be a positive number.";
        }
        public static class User
        {
            public const string UserNotFoundErrorMessage = "User not found.";
        }
        public static class RacketCart
        {
            public const string InvalidQuantityErrorMessage = "Invalid quantity. Please enter a valid number.";
        }
        public static class Ball
        {
            public const string BallNotFoundErrorMessage = "Ball not found.";
            public const string BallCannotBeNullErrorMessage = "Ball cannot be null.";

            public const string PriceRangeErrorMessage = "Price must be between 17 and 80.";
            public const string QuantityRangeErrorMessage = "Quantity must be a positive number.";
        }
        public static class BallCart
        {
            public const string InvalidQuantityErrorMessage = "Invalid quantity. Please enter a valid number.";
        }
        public static class Bag
        {
            public const string PriceRangeErrorMessage = "Price must be between 50.00 and 1000.00.";
            public const string QuantityErrorMessage = "Quality must be a positive number";

            public const string BagNotFoundErrorMessage = "Bag not found.";
            public const string BagCannotBeNullErrorMessage = "Bag cannot be null.";
        }
    }
}
