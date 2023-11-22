using System.Xml.Serialization;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext contextCar = new CarDealerContext();
            var inputJson = File.ReadAllText("../../../Datasets/suppliers.xml");
            Console.WriteLine(ImportParts(contextCar, inputJson));
        }
        private static Mapper GetMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<CarDealerProfile>());
            return new Mapper(cfg);
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer  xmlSerializer = new XmlSerializer(typeof(ImportSupplierDTO[]), 
                new XmlRootAttribute("Suppliers"));

            using var input = new StringReader(inputXml);

            ImportSupplierDTO[] importSupplierDTOs = (ImportSupplierDTO[])xmlSerializer.Deserialize(input);

            var mapper = GetMapper();
            Supplier[] suppliers = mapper.Map<Supplier[]>(importSupplierDTOs);

            context.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported: {suppliers.Length}";

        }
    }
}