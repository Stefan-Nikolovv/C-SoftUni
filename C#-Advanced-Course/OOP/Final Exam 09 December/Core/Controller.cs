using System.Text;
using System.Xml.Linq;
using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Repositories.Contracts;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private IRepository<IDiver> repositoryDiver;
        private IRepository<IFish> repositoryFish;

        public Controller()
        {
            repositoryDiver = new DiverRepository();
            repositoryFish = new FishRepository();
        }
        public string DiveIntoCompetition(string diverType, string diverName)
        {
            var diver = repositoryDiver.GetModel(diverName);

           if(diverType != "FreeDiver" && diverType != "ScubaDiver")
            {
                return $"{diverType} is not allowed in our competition.";
            }
           if(diver != null)
            {
                return $"{diverName} is already a participant -> DiverRepository.";
            }

            IDiver diverCreated = null;
            if(diverType == "FreeDiver")
            {
                diverCreated = new FreeDiver(diverName);
            }
            if(diverType == "ScubaDiver")
            {
                diverCreated = new ScubaDiver(diverName);
            }
            repositoryDiver.AddModel(diverCreated);

            return $"{diverName} is successfully registered for the competition -> DiverRepository.";
        }
        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            var searchedFish = repositoryFish.GetModel(fishName);
            if(fishType != "ReefFish" &&  fishType != "DeepSeaFish" && fishType != "PredatoryFish")
            {
                return $"{fishType} is forbidden for chasing in our competition.";
            }
            if(searchedFish != null)
            {
                return $"{fishName} is already allowed -> FishRepository.";
            }
            IFish fish = null;

            if(fishType == "ReefFish")
            {
                fish = new ReefFish(fishName, points);
            }
            if (fishType == "DeepSeaFish")
            {
                fish = new DeepSeaFish(fishName, points);
            }
            if (fishType == "PredatoryFish")
            {
                fish = new PredatoryFish(fishName, points);
            }
            repositoryFish.AddModel(fish);
            return $"{fishName} is allowed for chasing.";

        }
        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
           var serachedDiver = repositoryDiver.GetModel(diverName);
            var serachedFish = repositoryFish.GetModel(fishName);
            if(serachedDiver == null)
            {
                return $"DiverRepository has no {diverName} registered for the competition.";
            }
            if(serachedFish == null)
            {
                return $"{fishName} is not allowed to be caught in this competition.";
            }

            if(serachedDiver.HasHealthIssues == true)
            {
                return $"{diverName} will not be allowed to dive, due to health issues.";
            }
            if(serachedDiver.OxygenLevel < serachedFish.TimeToCatch)
            {
                serachedDiver.Miss(serachedFish.TimeToCatch);
                if (serachedDiver.OxygenLevel <= 0)
                {
                    serachedDiver.UpdateHealthStatus();
                  
                }

                return $"{diverName} missed a good {fishName}.";
            }
            if(serachedDiver.OxygenLevel == serachedFish.TimeToCatch)
            {
                if(!isLucky)
                {

                    serachedDiver.Miss(serachedFish.TimeToCatch);
                    if (serachedDiver.OxygenLevel <= 0)
                    {
                        serachedDiver.UpdateHealthStatus();
                    }
                    return $"{diverName} missed a good {fishName}.";
                }

                serachedDiver.Hit(serachedFish);
                if (serachedDiver.OxygenLevel <= 0)
                {
                    serachedDiver.UpdateHealthStatus();
                }

                return $"{diverName} hits a {serachedFish.Points}pt. {fishName}.";
            }

            serachedDiver.Hit(serachedFish);
            if (serachedDiver.OxygenLevel <= 0)
            {
                serachedDiver.UpdateHealthStatus();
            }
            return $"{diverName} hits a {serachedFish.Points}pt. {fishName}.";


        }
        public string HealthRecovery()
        {
            var allDivers = repositoryDiver.Models;
            List<IDiver> recovered = new List<IDiver>();
            foreach( var diver in allDivers)
            {
                if (diver.HasHealthIssues)
                {
                    diver.UpdateHealthStatus();
                    diver.RenewOxy();
                    recovered.Add(diver);
                }
            }
            return $"Divers recovered: {recovered.Count}";
        }
        public string DiverCatchReport(string diverName)
        {
            var specificDiver = repositoryDiver.GetModel(diverName);
            var sb = new StringBuilder();
            sb.AppendLine($"Diver [ Name: {specificDiver.Name}, " +
                $"Oxygen left: {specificDiver.OxygenLevel}," +
                $" Fish caught: {specificDiver.Catch.Count}, " +
                $"Points earned: {specificDiver.CompetitionPoints} ]");
            sb.AppendLine("Catch Report:");
            foreach (var fish in specificDiver.Catch)
            {
                var searchedFish = repositoryFish.GetModel(fish);
                sb.AppendLine($"{searchedFish.GetType().Name}: {searchedFish.Name} [ Points: {searchedFish.Points}, Time to Catch: {searchedFish.TimeToCatch} seconds ]");
            }
            return sb.ToString().TrimEnd();
        }
        public string CompetitionStatistics()
        {
            var sb = new StringBuilder();
            var infoOfAllDivers = repositoryDiver.Models
                .Where(x => x.HasHealthIssues == false)
                .OrderByDescending(cp => cp.CompetitionPoints)
                .ThenByDescending(c => c.Catch.Count)
                .ThenBy(n => n.Name)
                .ToList();

            sb.AppendLine("**Nautical-Catch-Challenge**");

            foreach (var diver in infoOfAllDivers)
            {
                sb.AppendLine(diver.ToString());
            }
            return sb.ToString().TrimEnd();
        }


       

        

        
    }
}