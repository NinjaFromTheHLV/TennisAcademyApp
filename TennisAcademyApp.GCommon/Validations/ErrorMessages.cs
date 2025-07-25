namespace TennisAcademyApp.GCommon.Validations
{
    public static class ErrorMessages
    {
        public static class Reservation
        {
            public const string PastDateErrorMessage = "Please select a valid date.";
            public const string SelectedTimeErrorMessage = "Reservation time must be between 08:00 and 20:00.";
            public const string FutureDateErrorMessage = "Reservations can be made for the next 14 days.";
            public const string SundayErrorMessage = "Sunday is off day! Please choose other time.";

            public const string DurationErrorMessage = "Duration must be either 60 or 120 minutes.";

            public const string CoachNotAvailableErrorMessage = "The selected coach is not available at the chosen time.";
        }
        public static class Coach
        {
            public const string AgeErrorMessage = "Age must be between 18 and 75!";

            public const string NameMaxLengthMessage = "Name cannot exceed 50 characters.";

            public const string DescriptionMinLengthMessage = "Description must be at least 10 characters.";
            public const string DescriptionMaxLengthMessage = "Description cannot exceed 150 characters.";
        }
    }
}
