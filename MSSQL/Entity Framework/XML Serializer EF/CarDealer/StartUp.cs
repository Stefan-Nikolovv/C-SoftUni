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
            //var inputJson = File.ReadAllText("../../../Datasets/suppliers.xml");
            //Console.WriteLine(ImportSuppliers(contextCar, inputJson));
            var inputJson = File.ReadAllText("../../../Datasets/cars.xml");
            Console.WriteLine(ImportCars(contextCar, inputJson));
            //var inputJson = File.ReadAllText("../../../Datasets/parts.xml");
            //Console.WriteLine(ImportParts(contextCar, inputJson));
        }
        private static Mapper GetMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<CarDealerProfile>());
            return new Mapper(cfg);
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
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

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCarsDTO[]), new XmlRootAttribute("Cars"));

            using StringReader input = new StringReader(inputXml);
            ImportCarsDTO[] importCarsDTOs = (ImportCarsDTO[])xmlSerializer.Deserialize(input);

            var mapper = GetMapper();

            List<Car> cars = new List<Car>();

            foreach(var item in importCarsDTOs)
            {
                Car car = mapper.Map<Car>(item);

                int[] carPartIds = item.PartsId
                    .Select(x => x.Id)
                    .Distinct()
                    .ToArray(); 

                var carParts = new List<PartCar>();

                foreach (var id in carPartIds)
                {
                    carParts.Add(new PartCar
                    {
                        Car = car,
                        PartId = id
                    });
                }
                car.PartsCars = carParts;
                cars.Add(car);
            }

            context.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml) 
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPartsDTO[])
                , new XmlRootAttribute("Parts"));

            using StringReader input = new StringReader(inputXml);

            ImportPartsDTO[] importPartsDTOs = (ImportPartsDTO[])xmlSerializer.Deserialize(input);

            var suppliedId = context.Suppliers.Select(x => x.Id).ToArray();


            var mapper = GetMapper();
            Part[] parts = mapper.Map<Part[]>(importPartsDTOs.Where(x => suppliedId.Contains(x.SupplierId)));

            context.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported: {parts.Length}";
        }


    }
}