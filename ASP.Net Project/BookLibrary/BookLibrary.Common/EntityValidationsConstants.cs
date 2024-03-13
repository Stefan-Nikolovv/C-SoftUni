namespace BookLibrary.Common
{
    public class EntityValidationsConstants
    {
        public static class Category
        {
            public const int CategoryNameMinLength = 5;
            public const int CategoryNameMaxLength = 15;
        }
        public static class Book
        {
            public const int TitleNameMinLength = 5;
            public const int TitleNameMaxLength = 25;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 150;

            public const string BookPriceMin = "0";
            public const string BookPriceMax = "2000";

            public const int BookPagesMin = 10;
            public const int BookPagesMax = 255;

            public const int PublisherMin = 10;
            public const int PublisherMax = 100;

            public const int LanguageMin = 2;
            public const int LanguageMax = 100;
        }

        public static class User
        {
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;

            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 15;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 15;
        }

        public static class Author
        {
            public const int AuthorPhoneNumberMin = 7;
            public const int AuthorPhoneNumberMax = 15;

            public const int AuthorFirstNameMinLength = 1;
            public const int AuthorFirstNameMaxLength = 15;

            public const int AuthorLastNameMinLength = 1;
            public const int AuthorLastNameMaxLength = 15;
        }
    }
}
