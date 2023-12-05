using System;
using NUnit.Framework;

namespace FootballTeam.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConstructorFootBallTeam()
        {
            var team = new FootballTeam("Pesho", 16);

            Assert.AreEqual(team.Name, "Pesho");

            Assert.AreEqual(team.Capacity, 16);
            Assert.AreEqual(team.Players.Count, 0);
        }
        [Test]
        public void TestConstructorFootBallTeamNameValidation()
        {
            

            Assert.Throws<ArgumentException>(() => new FootballTeam("", 16));

            Assert.Throws<ArgumentException>(() => new FootballTeam(null, 16));
        }


        [Test]
        public void TestConstructorFootBallTeamCapacityValidation()
        {
           

            Assert.Throws<ArgumentException>(() => new FootballTeam("Pesho", 14));
            Assert.Throws<ArgumentException>(() => new FootballTeam("", 0));
        }


        [Test]
        public void TestConstructorFootBallTeamAddNewPlayer()
        {
            var team = new FootballTeam("CSKA", 15);

            var player = new FootballPlayer("Pesho", 2, "Goalkeeper");

            for (int i = 0; i < 15; i++)
            {
                team.Players.Add(new FootballPlayer($"Pesho{i}", 2 + i, "Goalkeeper"));
            }

            team.AddNewPlayer(player);
            var expectedResult = team.AddNewPlayer(player);
            Assert.AreEqual(expectedResult, "No more positions available!");
        }
        [Test]
        public void TestConstructorFootBallTeamAddNewPlayerSuccessfull()
        {
            var team = new FootballTeam("CSKA", 15);

            var player = new FootballPlayer("Pesho", 2, "Goalkeeper");


           var actualResult =  team.AddNewPlayer(player);
            
            var expectedResult = $"Added player Pesho in position Goalkeeper with number 2";


            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestConstructorFootBallPickPlayerSuccessfull()
        {
            var team = new FootballTeam("CSKA", 15);

            var player = new FootballPlayer("Pesho", 2, "Goalkeeper");
            
                team.AddNewPlayer(player);

            Assert.AreEqual(team.PickPlayer("Pesho"), player);

        }

        [Test]
        public void TestConstructorFootBallPickPlayerUnSuccessfull()
        {
            var team = new FootballTeam("CSKA", 15);

            var player = new FootballPlayer("Peshko", 2, "Goalkeeper");

            team.AddNewPlayer(player);

            Assert.AreNotEqual(team.PickPlayer("Pesho"), player);

        }

        [Test]
        public void TestConstructorFootBallPlayerScoreSuccessfull()
        {
            var team = new FootballTeam("CSKA", 15);

            var player = new FootballPlayer("Peshko", 2, "Goalkeeper");

            team.AddNewPlayer(player);

            team.PlayerScore(2);

            var expectedResult = "Peshko scored and now has 1 for this season!";

            var actualt = $"{player.Name} scored and now has {player.ScoredGoals} for this season!";

            Assert.AreEqual(player.ScoredGoals, 1);
            Assert.AreEqual(expectedResult, actualt);

        }


    }
}