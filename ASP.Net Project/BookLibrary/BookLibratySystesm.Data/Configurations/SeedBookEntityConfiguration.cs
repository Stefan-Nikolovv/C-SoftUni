using BookLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;

namespace BookLibratySystesm.Data.Configurations
{
    public class SeedBookEntityConfiguration : IEntityTypeConfiguration<Book>
    {

        public void Configure(EntityTypeBuilder<Book> builder)
        {
            return;
            //builder.HasData(this.GenerateBooks());
        }

        //private  Book[] GenerateBooks()
        //{
        //    ICollection<Book> books = new HashSet<Book>();

        //    Book book;

        //    book = new Book()
        //    {

        //        Title = "Sample Book 1",
        //        Description = "This is a sample description for Book 1.",
        //        Price = 2100.00M,
        //        Pages = "100",
        //        Publisher = "Sample Publisher 1",
        //        Language = "English",
        //        CategoryId = 1,
        //        FileName = "Test",
        //        AuthorId = Guid.Parse("ca1523ec-643e-44fb-ae25-bae604c1bb9e"), //AgentId,
        //        LikerId = Guid.Parse("15F6ECC5-67F3-4354-B67C-35B9ABD8615C"), //AgentId
        //        CreatedOn = DateTime.UtcNow,
        //        Image =  GetSampleImageDataAsync() // Method to get sample image data

        //    };
        //    books.Add(book);
        //    return books.ToArray();
        //}

        //private static  byte[] GetSampleImageDataAsync()
        //{
        //    string currentDirectory = Directory.GetCurrentDirectory();
        //    //string parentDirectory = Directory.GetParent(currentDirectory).FullName;
        //    string imagePath = Path.Combine(currentDirectory, "wwwroot", "assets", "images", "brand", "brand-01.png");
        //    byte[] imageData;

        //    using (var fileStream = File.OpenRead(imagePath))
        //    using (var memoryStream = new MemoryStream())
        //    {
        //         fileStream.CopyToAsync(memoryStream);
        //        imageData = memoryStream.ToArray();
        //    }

        //    return imageData;
        //}
    }
}
