using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext contextDB = new ProductShopContext();
            //string inputJson = File.ReadAllText("../../../Datasets/users.json");
            //Console.WriteLine(ImportUsers(contextDB, inputJson
            //string inputJson = File.ReadAllText("../../../Datasets/products.json");
            //Console.WriteLine(ImportProducts(contextDB, inputJson));
            //string inputJson = File.ReadAllText("../../../Datasets/categories.json");
            //Console.WriteLine(ImportCategories(contextDB, inputJson));
            //string inputJson = File.ReadAllText("../../../Datasets/categories-products.json");
            //Console.WriteLine(ImportCategoryProducts(contextDB, inputJson));
            //Console.WriteLine(GetProductsInRange(contextDB));
            //Console.WriteLine(GetSoldProducts(contextDB));
            //Console.WriteLine(GetUsersWithProducts(contextDB));

        }


        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
          var users = JsonConvert.DeserializeObject <User[]>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson);
            var categoriesValid = categories.Where(c => c.Name is not null).ToArray();

            if(categoriesValid != null)
            {
                context.Categories.AddRange(categoriesValid);
                context.SaveChanges();
            }

            return $"Successfully imported {categoriesValid.Length}";

        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoriesProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoriesProducts.AddRange(categoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Length}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .OrderBy(p => p.name)
                
                .ToList();

                var json = JsonConvert.SerializeObject(products, Formatting.Indented);
                    return json;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts = context.Users
                .Where(p => p.ProductsSold.Any(p => p.BuyerId != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                    .Where(u => u.BuyerId != null)
                    .Select(u => new {
                        name = u.Name,
                        price = u.Price,
                        buyerFirstName = u.Seller!.FirstName,
                        buyerLastName = u.Seller.LastName,
                    })
                    .ToArray()
                })
                .ToArray();

            var json = JsonConvert.SerializeObject(soldProducts, Formatting.Indented);
            return json;
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersWithProducts = context.Users
                .Where(p => p.ProductsSold.Any(p => p.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = u.ProductsSold
                    .Where(p => p.BuyerId != null)
                    .Select(u => new
                    {
                        name = u.Name,
                        price = u.Price
                    })
                })
                .OrderByDescending(pr => pr.soldProducts.Count())
                .ToArray();

            var finalJson = new
            {
                userCount = usersWithProducts.Count(),
                user = usersWithProducts.Select(u => new
                {
                    u.firstName,
                    u.lastName,
                    u.age,
                    soldProducts = new
                    {
                        count = u.soldProducts.Count(),
                        products = u.soldProducts
                    }
                })
            };

            var json = JsonConvert.SerializeObject (finalJson, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            }); 
            return json;
            
        }
    }
}