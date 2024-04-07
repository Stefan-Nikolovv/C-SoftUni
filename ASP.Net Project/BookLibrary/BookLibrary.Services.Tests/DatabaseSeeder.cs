using BookLibrary.Data;
using BookLibrary.Data.Models;


namespace BookLibrary.Services.Tests
{
    public static class DatabaseSeeder
    {
       
        public static ApplicationUser AppTestUser;
        public static Author AuthorUser;
        public static Category Categories;
        public static Book Book;
        public static void SeedDatabase(BookDbContext dbContext)
        {


            AppTestUser = new ApplicationUser()
            {
                UserName = "Pesho",
                NormalizedUserName = "PESHO",
                Email = "pesho@agents.com",
                NormalizedEmail = "PESHO@AGENTS.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
                SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
                TwoFactorEnabled = false,
                FirstName = "Pesho",
                LastName = "Petrov",
                ProfilePicture = "123",
                LikedBooks = new HashSet<Book>()
            };


            AuthorUser = new Author()
            {
                
                FirstName = "Pesho",
                LastName = "Petrov",
                PhoneNumber = "0999292112",
                User = AppTestUser

            };

            Book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = "The First Seven",
                Description = "Book created about the story of Bulgarian mountain climber. ",
                Price = 2100.00M,
                Pages = "245",
                Publisher = "VackonPrime",
                Language = "Bulgarian",
                CategoryId = 3,
                AuthorId = Guid.Parse(AuthorUser.Id.ToString()), //AuthorId,
                LikerId = Guid.Parse(AppTestUser.Id.ToString()), //UserID
                CreatedOn = DateTime.UtcNow,
                Image = "2a46eb9a-6e55-42d3-8827-8059333c4b53_The First Seven.jpg"

            };

            
            dbContext.Users.Add(AppTestUser);
            dbContext.Authors.Add(AuthorUser);
            dbContext.Books.Add(Book);
            dbContext.SaveChanges();
        }
    }
}
