using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Common
{
   public static class EntityValidationConstants
    {
        public static class Category
        {
            public const int CategoryNameMinLength = 5;
            public const int CategoryNameMaxLength = 50;
        }

        public static class House
        {
            public const int HouseTitleMin = 10;
            public const int HouseTitleMax = 50;
            public const int HouseAddressMin = 30;
            public const int HouseAddressMax = 150;
            public const int HouseImageUrlLength = 2048;
            public const int HouseDescriptionMin = 50;
            public const int HouseDescriptionMax = 500;
            public const string HousePricePerMonthMin = "0";
            public const string HousePricePerMonthMax = "2000";
        }

        public static class Agent
        {
            public const int AgentPhoneNumberMin = 7;
            public const int AgentPhoneNumberMax = 15;
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
        
    }
}
