using BookLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BookLibratySystesm.Data.Configurations
{
    public class SeedBookEntityConfiguration : IEntityTypeConfiguration<Book>
    {

        public void Configure(EntityTypeBuilder<Book> builder)
        {
          
             builder.HasData(this.GenerateBooks());
        }

        private Book[] GenerateBooks()
        {
            ICollection<Book> books = new HashSet<Book>();

            Book book;

            book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = "The First Seven",
                Description = "Book created about the story of Bulgarian mountain climber. ",
                Price = 2100.00M,
                Pages = "245",
                Publisher = "VackonPrime",
                Language = "Bulgarian",
                CategoryId = 3,
                AuthorId = Guid.Parse("430E99FB-5A76-4235-9B39-83B13B17BB58"), //AuthorId,
                LikerId = Guid.Parse("794405D2-C399-41D9-9A60-C4E2E7CB1B67"), //UserID
                CreatedOn = DateTime.UtcNow,
                Image = "2a46eb9a-6e55-42d3-8827-8059333c4b53_The First Seven.jpg"

            };
            books.Add(book);
            book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = "Man's Search for Meaning",
                Description = "Everything can be taken from a man but one thing: the last of the human freedoms—to choose one’s attitude in any given set of circumstances, to choose",
                Price = 2100.00M,
                Pages = "233",
                Publisher = "amazonPrime",
                Language = "German",
                CategoryId = 2,
                AuthorId = Guid.Parse("430E99FB-5A76-4235-9B39-83B13B17BB58"), //AuthorId,
                LikerId = Guid.Parse("794405D2-C399-41D9-9A60-C4E2E7CB1B67"), //UserID
                CreatedOn = DateTime.UtcNow,
                Image = "107aba19-e84b-41e9-95bc-fefb811b9576_Man's Search for Meaning.jpg"

            };
            books.Add(book);
            book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = "Beyond Possible",
                Description = "Is 14 Peaks a book? Beyond Possible: One Man, Fourteen Peaks, and the Mountaineering Achievement of a Lifetime by ",
                Price = 2100.00M,
                Pages = "245",
                Publisher = "NetFlixPrime",
                Language = "English",
                CategoryId = 4,
                AuthorId = Guid.Parse("430E99FB-5A76-4235-9B39-83B13B17BB58"), //AuthorId,
                LikerId = Guid.Parse("794405D2-C399-41D9-9A60-C4E2E7CB1B67"), //UserID
                CreatedOn = DateTime.UtcNow,
                Image = "8ca6ff06-ac52-47bc-b625-7f9355953c69_Beyond Possible.jpg"

            };
            books.Add(book);
            return books.ToArray();
        }

       
        }
    }

