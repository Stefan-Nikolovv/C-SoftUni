namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.Data.Models.Enums;
    using Invoices.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using Invoices.Utilities;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";

        private static XmlHelper xmlHelper;

        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            xmlHelper = new XmlHelper();

            ImportClientDto[] clientDtos =
                xmlHelper.Deserialize<ImportClientDto[]>(xmlString, "Clients");


            ICollection<Client> validClients = new HashSet<Client>();
            foreach (var clientDto in clientDtos)
            {
                if (!IsValid(clientDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }

                ICollection<Address> alidAdresses = new HashSet<Address>();

                foreach (var addressDto in clientDto.Addresses)
                {
                    if (!IsValid(addressDto))
                    {
                        stringBuilder.AppendLine(ErrorMessage);
                        continue;
                    }
                    Address address = new Address()
                    {
                        StreetName = addressDto.StreetName,
                        StreetNumber = addressDto.StreetNumber,
                        PostCode = addressDto.PostCode,
                        City = addressDto.City,
                        Country = addressDto.Country,
                    };
                    alidAdresses.Add(address);

                }

                Client clients = new Client()
                {
                    Name = clientDto.Name,
                    NumberVat = clientDto.NumberVat,
                };
                validClients.Add(clients);
                stringBuilder.AppendLine(String.Format(SuccessfullyImportedClients, clients.Name, clients.NumberVat));
                
            }
            context.Clients.AddRange(validClients);
            context.SaveChanges();
            return stringBuilder.ToString().TrimEnd();
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            ImportInvoiceDto[] invoiceDtos = JsonConvert.DeserializeObject<ImportInvoiceDto[]>(jsonString);

            ICollection<Invoice> invoices = new HashSet<Invoice>();

            foreach (var invoiceDto in invoiceDtos)
            {
                if (!IsValid(invoiceDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }
                if (invoiceDto.DueDate < invoiceDto.IssueDate)
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }
                var invoicesToAdd = new Invoice
                {
                    Number = invoiceDto.Number,
                    IssueDate = invoiceDto.IssueDate,
                    DueDate = invoiceDto.DueDate,
                    ClientId = invoiceDto.ClientId,
                    Amount = invoiceDto.Amount,
                    CurrencyType = (CurrencyType)invoiceDto.CurrencyType,
                   
                };
                invoices.Add(invoicesToAdd);
                stringBuilder.AppendLine(String.Format(SuccessfullyImportedInvoices, invoicesToAdd.Number));
            }
            context.Invoices.AddRange(invoices);
            context.SaveChanges();
            return stringBuilder.ToString().TrimEnd();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {


            StringBuilder stringBuilder = new StringBuilder();

            ImportProductsDto[] productDtos = JsonConvert.DeserializeObject<ImportProductsDto[]>(jsonString);

            ICollection<Product> validProducts = new HashSet<Product>();
            ICollection<int> clientIdDB = context.Clients
                .Select(x => x.Id)
                .ToArray();

            foreach (var productsDto in productDtos)
            {
                if (!IsValid(productsDto))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }
                Product product = new Product()
                {
                    Name = productsDto.Name,
                    Price = productsDto.Price,
                    CategoryType = (CategoryType)productsDto.CategoryType,

                };
                
                foreach (var id in productsDto.Clients.Distinct())
                {
                    if (!clientIdDB.Contains(id))
                    {
                        stringBuilder.AppendLine(ErrorMessage);
                        continue;
                    }
                    product.ProductsClients.Add(new ProductClient()
                    {
                        ClientId = id,
                    });

                }
                validProducts.Add(product);
                stringBuilder.AppendLine(String.Format(SuccessfullyImportedProducts, product.Name, product.ProductsClients.Count));
            }

            context.Products.AddRange(validProducts);
            context.SaveChanges();
            return stringBuilder.ToString().TrimEnd();

        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    } 
}
