namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Linq;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.Data.Models.Enums;
    using Boardgames.DataProcessor.ImportDto;
    using Boardgames.Utilities;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        private static XmlHelper xmlHelper;
        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            xmlHelper = new XmlHelper();
            StringBuilder stringBuilder = new StringBuilder();

            ImportCreatorDto[] importCreatorDtos = xmlHelper.Deserialize<ImportCreatorDto[]>(xmlString, "Creators");

            ICollection<Creator> validCreator = new HashSet<Creator>();

            foreach (var creatorDto in importCreatorDtos)
            {
                if (!IsValid(creatorDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Creator creator = new()
                {
                    FirstName = creatorDto.FirstName,
                    LastName = creatorDto.LastName
                };

                foreach (var boardGameDto in creatorDto.importBoardGames)
                {
                    if (!IsValid(boardGameDto))
                    {
                        stringBuilder.AppendLine("Invalid data!");
                        continue;
                    }

                    creator.Boardgames.Add(new Boardgame()
                    {
                        Name = boardGameDto.Name,
                        Rating = boardGameDto.Rating,
                        YearPublished = boardGameDto.YearPublished,
                        CategoryType = (CategoryType)boardGameDto.CategoryType,
                        Mechanics = boardGameDto.Mechanics
                    });
                }

                validCreator.Add(creator);
                stringBuilder.AppendLine(string.Format(SuccessfullyImportedCreator, creator.FirstName,
                    creator.LastName, creator.Boardgames.Count()));
            }

            context.Creators.AddRange(validCreator);
            context.SaveChanges();


            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            ImportSellerDto[] importSellerDtos = JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString);

            HashSet<Seller> validSelers = new HashSet<Seller>();
            var boardGameIds = context.Boardgames
                    .Select(x => x.Id)
                    .ToArray();

            foreach (var importSellerDto in importSellerDtos)
            {
                if (!IsValid(importSellerDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                Seller seller = new Seller()
                {
                    Name = importSellerDto.Name,
                    Address = importSellerDto.Address,
                    Country = importSellerDto.Country,
                    Website = importSellerDto.Website,
                };
                

                foreach (var boardGameId in importSellerDto.Boardgames.Distinct())
                {
                    if (!boardGameIds.Contains(boardGameId))
                    {
                        stringBuilder.AppendLine(ErrorMessage);
                        continue;
                    }

                    BoardgameSeller bgs = new()
                    {
                        Seller = seller,
                        BoardgameId = boardGameId
                    };

                    seller.BoardgamesSellers.Add(bgs);

                    
                }
                validSelers.Add(seller);
                stringBuilder.AppendLine(String.Format(SuccessfullyImportedSeller, seller.Name, seller.BoardgamesSellers.Count()));
            }
            context.Sellers.AddRange(validSelers);
            context.SaveChanges();
            return stringBuilder.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
