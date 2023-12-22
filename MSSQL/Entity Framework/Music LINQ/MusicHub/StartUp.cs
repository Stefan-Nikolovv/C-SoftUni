namespace MusicHub
{
    using System;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            //DbInitializer.ResetDatabase(context);
            Console.WriteLine(ExportAlbumsInfo(context, 9));
            //Test your solutions here
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var producers = context.Producers
                .Include(x => x.Albums)
                .ThenInclude(x => x.Songs)
                .ThenInclude(x => x.Writer)
                .FirstOrDefault(x => x.Id == producerId)
                .Albums.Select(x => new
                {
                    AlbumName = x.Name,
                    ReleaseDate = x.ReleaseDate,
                    ProducerName = x.Producer.Name,
                    AlbulmSongs = x.Songs.Select(x => new
                    {
                       SongName =  x.Name,
                       SongPrice = x.Price,
                       SongWriterName = x.Writer.Name
                    })
                    .OrderByDescending(x => x.SongName)
                    .ThenBy(x => x.SongWriterName),
                    TotalAlbumPrice = x.Price,

                }).OrderByDescending(x => x.TotalAlbumPrice);
            StringBuilder sb = new StringBuilder();
            foreach (var producer in producers)
            {
                sb.AppendLine($"--AlbumName: {producer.AlbumName}")
                  .AppendLine($"-ReleaseDate: {producer.ReleaseDate}")
                  .AppendLine($"-ProducerName {producer.ProducerName}")
                  .AppendLine($"-Songs:");

                if (producer.AlbulmSongs.Any())
                {
                    int counter = 1;
                    foreach (var song in producer.AlbulmSongs)
                    {
                        sb
                            .AppendLine($"---# {counter}")
                            .AppendLine($"---SongName: {song.SongName}")
                            .AppendLine($"---Price: {song.SongPrice:f2}")
                            .AppendLine($"---Writer: {song.SongWriterName}");

                    }
                }
                sb.AppendLine($"AlbumPrice: {producer.TotalAlbumPrice:f2}");
            }


            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            throw new NotImplementedException();
        }
    }
}
