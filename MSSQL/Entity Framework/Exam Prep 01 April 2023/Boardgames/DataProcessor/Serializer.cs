namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.Data.Models.Enums;
    using Boardgames.DataProcessor.ExportDto;
    using Boardgames.Utilities;
    using Newtonsoft.Json;

    public class Serializer
    {
        private static XmlHelper xmlHelper;
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            xmlHelper = new XmlHelper();
            var creators = context.Creators
                .Where(c => c.Boardgames.Any())
                .Select(c => new ExportCreatorDto()
                {
                    CreatorName = c.FirstName + " " + c.LastName,
                    BoardgamesCount = c.Boardgames.Count(),
                    Boardgames = c.Boardgames
                    .Select(bg => new ExportXmlBoardGameDto()
                    {
                        BoardgameName = bg.Name,
                        BoardgameYearPublished = bg.YearPublished,
                    })
                    .OrderBy(bg => bg.BoardgameName)
                    .ToArray()
                    
                }).OrderByDescending(c => c.BoardgamesCount)
                .ThenBy(c => c.CreatorName)
                .ToArray();

            return xmlHelper.Serialize(creators, "Creators");

        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var sellers = context.Sellers
                .Where(s => s.BoardgamesSellers.Any(x => x.Boardgame.YearPublished >= year && x.Boardgame.Rating <= rating))
                .Select(s => new ExportSellersDto()
                {
                    Name = s.Name,
                    Website = s.Website,
                    Boardgames = s.BoardgamesSellers
                    .Where(x => x.Boardgame.YearPublished >= year && x.Boardgame.Rating <= rating)
                    .Select(x => new ExporBoardGames()
                    {
                        Name = x.Boardgame.Name,
                        Rating = x.Boardgame.Rating,
                        Mechanics = x.Boardgame.Mechanics,
                        Category = x.Boardgame.CategoryType.ToString(),
                    })
                    .OrderByDescending(x => x.Rating)
                    .ThenBy(x => x.Name)
                    .ToArray()
                }).OrderByDescending(s => s.Boardgames.Length)
                .ThenBy(s => s.Name)
                .Take(5)
                .ToArray();

                return JsonConvert.SerializeObject(sellers, Formatting.Indented);
        }
    }
}