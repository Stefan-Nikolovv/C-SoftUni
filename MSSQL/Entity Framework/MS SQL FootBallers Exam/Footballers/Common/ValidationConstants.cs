
namespace Footballers.Common
{
    public static class ValidationConstants
    {
        //Footballer
        public const int FootballerNameMaxLength = 40;
        public const int FootballerNameMinLength = 2;
        public const int FootballerPositionTypeMaxLength = 3;
        public const int FootballerPositionTypeMinLength = 0;
        public const int FootballerBestSkillTypeMaxLength = 4;
        public const int FootballerBestSkillMinLength = 0;

        //Team
        public const int TeamNameMaxLength = 40;
        public const int TeamNameMinLength = 3;
        public const string TeamNameRegex = @"^[A-Za-z0-9\s\.\-]{3,}$";
        public const int TeamNationalityMaxLength = 40;
        public const int TeamNationalityMinLength = 2;
        //Coach
        public const int FootballerCoachMaxLength = 40;
        public const int FootballerCoachMinLength = 2;
    }
}
