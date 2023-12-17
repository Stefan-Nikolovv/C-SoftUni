namespace Invoices.DataProcessor
{
    using System.Globalization;
    using Invoices.Data;
    using Invoices.Data.Models.Enums;
    using Invoices.DataProcessor.ExportDto;
    using Invoices.Extensions;
    using Invoices.Utilities;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Serializer
    {
        private static XmlHelper xmlHelper;
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            xmlHelper = new XmlHelper();
            var clinetInvoices = context.Clients
                .Where(c => c.Invoices.Any(i => i.IssueDate > date))
                .Select(c => new ExportClientXmlDto()
                {
                    ClientName = c.Name,
                    VatNumber = c.NumberVat,
                    InvoicesCount = c.Invoices.Count,
                    Invoices = c.Invoices
                    .OrderBy(i => i.IssueDate)
                    .ThenByDescending(i => i.DueDate)
                    .Select(i => new ExportInvoiceDto()
                    {
                        InvoiceNumber = i.Number,
                        DueDate = i.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        Currency = i.CurrencyType.ToString(),
                        InvoiceAmount = decimal.Parse(i.Amount.ToString("0.##")),
                    })
                    .ToArray()


                })
                .OrderByDescending(i => i.InvoicesCount)
                .ThenBy(i => i.ClientName)
                .ToArray();

            return xmlHelper.Serialize(clinetInvoices, "Clients");
        }

        

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {

            var productDtos = context.Products
                .Include(p => p.ProductsClients)
                .ThenInclude(pc => pc.Client)
                .Where(p => p.ProductsClients.Any(c => c.Client.Name.Length >= nameLength))
                .Select(p => new ExportProductDto
                {
                    Name = p.Name,
                    Price = decimal.Parse(p.Price.ToString("0.##")),
                    Category = p.CategoryType,
                    Clients = p.ProductsClients
                    .Where(pc => pc.Client.Name.Length >= nameLength)
                    .Select(c => new ExportClientDto
                    {
                        Name = c.Client.Name,
                        NumberVat = c.Client.NumberVat,
                    })
                    .OrderBy(c => c.Name)
                    .ToArray()

                }).OrderByDescending(p => p.Clients.Length)
                .ThenBy(p => p.Name)
                .ToArray()
                .Take(5);
         

            return productDtos.SerializeToJson();
        }
    }
}