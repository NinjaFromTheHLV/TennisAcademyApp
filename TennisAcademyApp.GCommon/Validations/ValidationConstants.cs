namespace TennisAcademyApp.GCommon.Validations
{
    public static class ValidationConstants
    {
        public static class Coach
        {
            public const int CoachNameMinLenght = 5;
            public const int CoachNameMaxLenght = 50;

            public const int CoachAgeMinLenght = 18;
            public const int CoachAgeMaxLenght = 75;

            public const int CoachDescriptionMinLenght = 10;
            public const int CoachDescriptionMaxLenght = 150;
        }
        public static class Reservation
        {
            public const int PlayerNotesMaxLenght = 70;
        }
    }
}
