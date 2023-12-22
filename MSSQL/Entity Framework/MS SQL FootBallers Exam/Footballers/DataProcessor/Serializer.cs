namespace Footballers.DataProcessor
{
    using System.Globalization;
    using Data;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ExportDto;
    using Footballers.Extensions;
    using Footballers.Utilities;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class Serializer
    {
        private static XmlHelper xmlHelper;
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            xmlHelper = new();

            var coaches = context.Coaches
                .Where(c => c.Footballers.Any())
                .Select(c => new ExportCoachDto()
                {
                    CoachName = c.Name,
                    FootballersCount = c.Footballers.Count(),
                    Footballers = c.Footballers
                    .Select(f => new ExportFootBallerDto()
                    {
                        Name = f.Name,
                        Position = (PositionType)f.PositionType,
                    })
                    .OrderBy(f => f.Name)
                    .ToArray()

                }).OrderByDescending(c => c.FootballersCount)
                .ThenBy(c => c.CoachName)
                .ToArray();

            return xmlHelper.Serialize(coaches, "Coaches");

        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teams = context.Teams
                .Where(t => t.TeamsFootballers.Any(tf => tf.Footballer.ContractStartDate >= date))
                .ToArray()
                .Select(t => new ExportTeamDto()
                {
                    Name = t.Name,
                    Footballers = t.TeamsFootballers
                    .Where(tf => tf.Footballer.ContractStartDate >= date)
                    .OrderByDescending(t => t.Footballer.ContractEndDate)
                    .ThenBy(t => t.Footballer.Name)
                    .Select(t => new ExportFootBallersDto()
                    {
                        FootballerName = t.Footballer.Name,
                        ContractStartDate = t.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                        ContractEndDate = t.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                        BestSkillType = (BestSkillType)t.Footballer.BestSkillType,
                        PositionType = (PositionType)t.Footballer.PositionType
                    })
                    .ToArray()
                })
                .OrderByDescending(t => t.Footballers.Length)
                .ThenBy(t => t.Name)
                .Take(5)
                .ToArray();
            //var teams = context
            //    .Teams
            //    .Where(t => t.TeamsFootballers.Any(tf => tf.Footballer.ContractStartDate >= date))
            //    .ToArray()
            //    .Select(t => new
            //    {
            //        t.Name,
            //        Footballers = t.TeamsFootballers
            //            .Where(tf => tf.Footballer.ContractStartDate >= date)
            //            .ToArray()
            //            .OrderByDescending(tf => tf.Footballer.ContractEndDate)
            //            .ThenBy(tf => tf.Footballer.Name)
            //            .Select(tf => new
            //            {
            //                FootballerName = tf.Footballer.Name,
            //                ContractStartDate = tf.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
            //                ContractEndDate = tf.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
            //                BestSkillType = tf.Footballer.BestSkillType.ToString(),
            //                PositionType = tf.Footballer.PositionType.ToString()
            //            })
            //            .ToArray()
            //    })
            //    .OrderByDescending(t => t.Footballers.Length)
            //    .ThenBy(t => t.Name)
            //    .Take(5)
            //    .ToArray();

            return teams.SerializeToJson();


        }
    }
}
