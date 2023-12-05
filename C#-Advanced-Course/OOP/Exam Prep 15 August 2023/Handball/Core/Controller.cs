

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;

namespace Handball.Core
{
    public class Controller : IController
    {
        private IRepository<IPlayer> playerRepositories;
        private IRepository<ITeam> teamRepositories;

        public Controller()
        {
            playerRepositories = new PlayerRepository();
            teamRepositories = new TeamRepository();
        }
        public string LeagueStandings()
        {
           
            var sb = new StringBuilder();

            sb.AppendLine($"***League Standings***");
            List<ITeam> sortedTeams = teamRepositories.Models.OrderByDescending(t => t.PointsEarned)
                .OrderByDescending(t => t.OverallRating)
                .ThenBy(t => t.Name)
                .ToList();

            foreach (var team in sortedTeams)
            {
                sb.AppendLine(team.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string NewContract(string playerName, string teamName)
        {
            if(!playerRepositories.ExistsModel(playerName))
            {
                return $"Player with the name {playerName} does not exist in the {nameof(PlayerRepository)}.";
            }

            if(!teamRepositories.ExistsModel(teamName))
            {
                return $"Team with the name {teamName} does not exist in the {teamRepositories.GetType().Name}.";
            }
          IPlayer player = playerRepositories.GetModel(playerName);
          ITeam team = teamRepositories.GetModel(teamName);
            if (player.Team != null)
            {
                return $"Player {playerName} has already signed with {player.Team}.";
            }
            player.JoinTeam(teamName);
            team.SignContract(player);

            return $"Player {playerName} signed a contract with {teamName}.";
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            var firstTeam = teamRepositories.GetModel(firstTeamName);
            var secondTeam = teamRepositories.GetModel(secondTeamName);

            if(firstTeam.OverallRating > secondTeam.OverallRating)
            {
                firstTeam.Win();
                secondTeam.Lose();
                return $"Team {firstTeamName} wins the game over {secondTeamName}!";
            }
            else if(firstTeam.OverallRating < secondTeam.OverallRating)
            {
                secondTeam.Win();
                firstTeam.Lose();
               
                return $"Team {secondTeamName} wins the game over {firstTeamName}!";
            }
                firstTeam.Draw();
                secondTeam.Draw();
               
            
            return $"The game between {firstTeamName} and {secondTeamName} ends in a draw!";
        }

        public string NewPlayer(string typeName, string name)
        {
            if(typeName != "Goalkeeper" && typeName != "CenterBack" && typeName != "ForwardWing")
            {
                return $"{typeName} is invalid position for the application.";
            }
            if (playerRepositories.ExistsModel(name))

            {
                IPlayer existingPlayer = playerRepositories.GetModel(name);
                return $"{name} is already added to the {playerRepositories.GetType().Name} as {existingPlayer.GetType().Name}.";
            }

            IPlayer player = null;

            if(typeName == "Goalkeeper")
            {
                player = new Goalkeeper(name);
            }
            else if(typeName == "CenterBack")
            {
                player = new CenterBack(name);
            }
            else if(typeName == "ForwardWing")
            {
                player = new ForwardWing(name);
            }

            playerRepositories.AddModel(player);
            return $"{name} is filed for the handball league.";

        }

        public string NewTeam(string name)
        {
            Team team = new Team(name);

            if (teamRepositories.ExistsModel(name))
            {
                return $"{name} is already added to the TeamRepository.";
            }
            teamRepositories.AddModel(team);
            return $"{name} is successfully added to the TeamRepository.";

        }

        public string PlayerStatistics(string teamName)
        {
           ITeam team = teamRepositories.GetModel(teamName);
            var sb = new StringBuilder();

            sb.AppendLine($"***{teamName}***");
            List<IPlayer> players = team.Players
                .OrderByDescending(p => p.Rating)
                .ThenBy(p => p.Name)
                .ToList();

            foreach ( var player in players )
            {
                sb.AppendLine(player.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
