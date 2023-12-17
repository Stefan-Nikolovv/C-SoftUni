namespace Trucks.DataProcessor
{
    using System.Text.Json.Nodes;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ExportDto;
    using Trucks.Utilities;

    public class Serializer
    {
        private static XmlHelper xmlHelper;
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            xmlHelper = new XmlHelper();

            ExportDespatcherDto[] despacherTrucks = context.Despatchers
                 .Where(d => d.Trucks.Any())
                 .Select(d => new ExportDespatcherDto()
                 {
                     DespatcherName = d.Name,
                     TrucksCount = d.Trucks.Count(),
                     Trucks = d.Trucks
                     .Select(t => new ExportTruckDto()
                     {
                         RegistrationNumber = t.RegistrationNumber,
                         Make = t.MakeType.ToString(),
                     })
                     .OrderBy(t => t.RegistrationNumber)
                     .ToArray()
                 })
                 .OrderByDescending(d => d.TrucksCount)
                 .ThenBy(d => d.DespatcherName)
                 .ToArray();

            return xmlHelper.Serialize(despacherTrucks, "Despatchers");
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            var clients = context.Clients
                .Include(c => c.ClientsTrucks)
                .ThenInclude(ct => ct.Truck)
                .AsNoTracking()
                .ToArray()
                  .Where(c => c.ClientsTrucks.Any(x => x.Truck.TankCapacity >= capacity))
                  .Select(t => new
                  {
                      t.Name,
                      Trucks = t.ClientsTrucks.Where(ct => ct.Truck.TankCapacity >= capacity)
                      .Select(ct => new
                      {
                          TruckRegistrationNumber = ct.Truck.RegistrationNumber,
                          VinNumber = ct.Truck.VinNumber,
                          TankCapacity = ct.Truck.TankCapacity,
                          CargoCapacity = ct.Truck.CargoCapacity,
                          CategoryType = ct.Truck.CategoryType.ToString(),
                          MakeType = ct.Truck.MakeType.ToString()
                      })
                      .OrderBy(t => t.MakeType)
                      .ThenByDescending(t => t.CargoCapacity)
                      .ToArray()
                  })
                  .OrderByDescending(c => c.Trucks.Length)
                  .ThenBy(c => c.Name)
                  .ToArray()
                  .Take(10);


                return JsonConvert.SerializeObject(clients, Formatting.Indented);
        }
    }
}
