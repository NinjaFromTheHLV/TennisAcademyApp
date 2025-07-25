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
    }
}
