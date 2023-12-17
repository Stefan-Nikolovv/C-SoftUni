using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Repositories.Contracts;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private IRepository<IPilot> pilotRepository;
        private IRepository<IRace> raceRepository;
        private IRepository<IFormulaOneCar> carRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            carRepository = new FormulaOneCarRepository();
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            var searchedPilot = pilotRepository.FindByName(pilotName);
            
            if(searchedPilot == null || searchedPilot.Car != null)
            {
                throw new InvalidOperationException($"Pilot {pilotName} does not exist or has a car.");
            }
            var searchedModel = carRepository.FindByName(carModel);
            if (searchedModel == null)
            {
                throw new NullReferenceException($"Car {carModel} does not exist.");
            }

            searchedPilot.AddCar(searchedModel);
            carRepository.Remove(searchedModel);
            return $"Pilot {pilotName} will drive a {searchedModel.GetType().Name} {carModel} car.";

        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            var searchedRace = raceRepository.FindByName(raceName);
            if (searchedRace == null ) 
            {
                throw new NullReferenceException($"Race {raceName} does not exist.");
            }
            var searchedPilot = pilotRepository.FindByName(pilotFullName);
           
           
            if(searchedPilot == null || searchedPilot.CanRace == false)
            {
                throw new InvalidOperationException($"Can not add pilot {pilotFullName} to the race.");
            }
          
            searchedRace.AddPilot(searchedPilot);
            
            return $"Pilot {pilotFullName} is added to the {raceName} race.";
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            
            var searchedCar = carRepository.FindByName(model);
           if(searchedCar != null)
            {
                return $"Formula one car {model} is already created.";
            }
           if(type != "Williams" && type != "Ferrari")
            {
                throw new InvalidOperationException($"Formula one car type {type} is not valid.");
            }

            IFormulaOneCar car = null;

           if(type == "Williams")
            {
                car = new Williams(model, horsepower, engineDisplacement);
               
               
            }
            else if (type == "Ferrari")
            {
                car = new Ferrari(model, horsepower, engineDisplacement);
                
            }

           carRepository.Add(car);

            return $"Car {type}, model {model} is created.";
        }

        public string CreatePilot(string fullName)
        {
           IPilot searchedPilot = pilotRepository.FindByName(fullName);
            if(searchedPilot != null)
            {
                throw new InvalidOperationException($"Pilot {fullName} is already created.");
            }
            Pilot pilot = new Pilot(fullName);
            pilotRepository.Add(pilot);
            return $"Pilot {fullName} is created.";

        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            var searchedRace = raceRepository.FindByName(raceName);
            if(searchedRace != null)
            {
                throw new InvalidOperationException($"Race { raceName } is already created.");
            }
            IRace race = new Race(raceName, numberOfLaps);
            raceRepository.Add(race);
            return $"Race {raceName} is created.";
        }

        public string PilotReport()
        {

            var orderedPilots = pilotRepository.Models.OrderByDescending(p => p.NumberOfWins).ToList();
            var sb = new StringBuilder();

            foreach (var pilot in orderedPilots)
            {
                sb.AppendLine(pilot.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
           
            var race = raceRepository.Models.Where(r => r.TookPlace).ToList();
            var sb =  new StringBuilder();

            foreach (var item in race)
            {
                sb.AppendLine(item.RaceInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string StartRace(string raceName)
        {
            var isRaceValid = raceRepository.FindByName(raceName);
            if(isRaceValid == null)
            {
                throw new NullReferenceException($"Race {raceName} does not exist.");
            }
            if(isRaceValid.Pilots.Count < 3)
            {
                throw new InvalidOperationException($"Race {raceName} cannot start with less than three participants.");
            }

            if (isRaceValid.TookPlace)
            {
                throw new InvalidOperationException($"Can not execute race {raceName }.");
            }

            List<IPilot> orderedPilots = isRaceValid.Pilots
                .OrderByDescending(p => p.Car.RaceScoreCalculator(isRaceValid.NumberOfLaps))
              
                .ToList();
            isRaceValid.TookPlace = true;
            orderedPilots[0].WinRace();
            var sb = new StringBuilder();
            sb.AppendLine($"Pilot {orderedPilots[0].FullName} wins the { raceName } race.");
            sb.AppendLine($"Pilot {orderedPilots[1].FullName} is second in the {raceName} race.");
            sb.AppendLine($"Pilot {orderedPilots[2].FullName} is third in the {raceName} race.");
            return sb.ToString().TrimEnd();
        }
    }
}