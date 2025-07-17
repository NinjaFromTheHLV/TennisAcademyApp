namespace TennisAcademyApp.ViewModels.Validations
{
    public static class CoachInputModelValidations
    {
        public const string RequiredNameMessage = "Name is required.";
        public const string NameMinLengthMessage = "Name must be at least 5 characters.";
        public const string NameMaxLengthMessage = "Name cannot exceed 50 characters.";

        public const string AgeRequiredMessage = "Age is required.";
        public const string AgeErrorMessage = "Age must be between 18 and 75!";

        public const string DescriptionRequiredMessage = "Description is required.";
        public const string DescriptionMinLengthMessage = "Description must be at least 10 characters.";
        public const string DescriptionMaxLengthMessage = "Description cannot exceed 150 characters.";
    }
}
