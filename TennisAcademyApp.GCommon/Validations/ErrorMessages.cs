namespace TennisAcademyApp.GCommon.Validations
{
    public static class ErrorMessages
    {
        public static class Reservation
        {
            public const string PastDateErrorMessage = "Please select a valid date.";
            public const string TwoHoursErrorMessage = "Reservations can be made at least two hours from now.";    
            public const string SelectedTimeErrorMessage = "Reservation time must be between 08:00 and 20:00.";
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

            public const string NameMaxLengthMessage = "Name cannot exceed 50 characters.";

            public const string DescriptionMinLengthMessage = "Description must be at least 10 characters.";
            public const string DescriptionMaxLengthMessage = "Description cannot exceed 150 characters.";

            public const string CoachNotFoundErrorMessage = "Coach not found."; 
        }
        public static class Racket
        {
            public const string RacketNotFoundErrorMessage = "Racket not found.";
            public const string RacketCannotBeNullErrorMessage = "Racket cannot be null.";

            public const string PriceRangeErrorMessage = "Price must be between 30 and 1500.";
        }
        public static class User
        {
            public const string UserNotFoundErrorMessage = "User not found.";
        }
        public static class RacketCart
        {
            public const string InvalidQuantityErrorMessage = "Invalid quantity. Please enter a valid number.";
        }
    }
}
