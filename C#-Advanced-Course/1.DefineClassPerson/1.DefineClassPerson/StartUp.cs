

using System;

namespace DefiningClasses;


public class Startup
{
    static void Main(string[] args)
    {
        List<Car> cars = new();

        
        int countCars = int.Parse(Console.ReadLine());

        for (int i = 0; i < countCars; i++)
        {
            string[] carProperties = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);
           foreach (string carProperty in carProperties)
            {
                Console.WriteLine(carProperty);
            }
            string modelOf = carProperties[0];
            int engineSpeed = int.Parse(carProperties[1]);
            int enginePower = int.Parse(carProperties[2]);
              int coargoWeight = int.Parse(carProperties[3]);
             string coargoType = carProperties[4];
            double tyre1 = double.Parse(carProperties[5]);
              int tyre1Age = int.Parse(carProperties[6]);
            double tyre2 = double.Parse(carProperties[7]);
             int tyre2Age = int.Parse(carProperties[8]);
            double tyre3 = double.Parse(carProperties[9]);
              int tyre3Age = int.Parse(carProperties[10]);
            double tyre4 = double.Parse(carProperties[11]);
            int tyre4Age = int.Parse(carProperties[12]);

            Car car = new(modelOf,
                engineSpeed,
                enginePower,
                coargoWeight,
                coargoType,
                tyre1, 
                tyre1Age,
                tyre2, 
                tyre2Age,
                tyre3,
                tyre3Age,
                tyre4,
                tyre4Age
                );

            cars.Add(car);
        }
        string command = Console.ReadLine();

        string[] filteredCarModels;

        if (command == "fragile" ) 
 
        {
            filteredCarModels = cars
                .Where(c => c.Cargo.Type == "fragile" && c.Tyres.Any(t => t.Pressure < 1))
                .Select(c => c.Model)
                .ToArray();
        }
        else
        {
            filteredCarModels = cars
                .Where(c => c.Cargo.Type == "flammable" && c.Engine.Power > 250)
                .Select(c => c.Model)
                .ToArray();
        }
        Console.WriteLine(string.Join(Environment.NewLine, filteredCarModels));
    }
}
