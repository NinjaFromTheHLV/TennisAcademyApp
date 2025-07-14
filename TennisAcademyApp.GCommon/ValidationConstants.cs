namespace TennisAcademyApp.GCommon
{
    public static class ValidationConstants
    {
        public static class Coach
        {
            public const int CoachNameMinLenght = 5;
            public const int CoachNameMaxLenght = 35;

            public const int CoachDescriptionMinLenght = 10;
            public const int CoachDescriptionMaxLenght = 100;
        }
        public static class Reservation
        {
            public const int PlayerNotesMaxLenght = 50;
        }
    }
}
