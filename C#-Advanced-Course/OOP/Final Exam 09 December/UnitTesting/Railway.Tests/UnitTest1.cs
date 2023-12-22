namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConstructorSetAllProperties()
        {
            RailwayStation station = new RailwayStation("TestStation");

            Assert.AreEqual("TestStation", station.Name);
            Assert.AreEqual(0, station.ArrivalTrains.Count);
            Assert.AreEqual(0, station.DepartureTrains.Count);
        }

        [Test]
        public void TestNameThowesAnError()
        {
            Assert.Throws<ArgumentException>(() => new RailwayStation(null));
            Assert.Throws<ArgumentException>(() => new RailwayStation(""));
        }

        [Test]
        public void TestNewArrivalOnBoardMethod()
        {
            RailwayStation station = new RailwayStation("TestStation");
            string trainInfo1 = "Train1";
            string trainInfo2 = "Train2";

          
            station.NewArrivalOnBoard(trainInfo1);
            station.NewArrivalOnBoard(trainInfo2);

        
            Assert.AreEqual(2, station.ArrivalTrains.Count);
            Assert.AreEqual(trainInfo1, station.ArrivalTrains.Peek());
        }

        [Test]
        public void TestTrainHasArrivedMethodFirstReturn()
        {
            RailwayStation station = new RailwayStation("TestStation");
           
            string trainInfo1 = "Train1";
            string trainInfo2 = "Train2";
            station.NewArrivalOnBoard(trainInfo1);
            station.NewArrivalOnBoard(trainInfo2);

            string actualResult = station.TrainHasArrived(trainInfo2);


            Assert.AreEqual(2, station.ArrivalTrains.Count);
            Assert.AreEqual(0, station.DepartureTrains.Count);
            Assert.AreEqual(actualResult, $"There are other trains to arrive before {trainInfo2}.");
        }

        [Test]
        public void TestTrainHasArrivedMethodSecondReturn()
        {
            RailwayStation station = new RailwayStation("TestStation");
            string trainInfo = "Train1";
            station.NewArrivalOnBoard(trainInfo);

            // Act
            string result = station.TrainHasArrived(trainInfo);

            // Assert
            Assert.AreEqual(0, station.ArrivalTrains.Count);
            Assert.AreEqual(1, station.DepartureTrains.Count);
           
            Assert.AreEqual(result, "Train1 is on the platform and will leave in 5 minutes.");
        }

        [Test]
        public void TestTrainHasLeftMethodToReturnTrue()
        {
            RailwayStation station = new RailwayStation("TestStation");
            string trainInfo = "Train";
            station.NewArrivalOnBoard(trainInfo);
            station.TrainHasArrived(trainInfo);

            
            bool result = station.TrainHasLeft(trainInfo);

            
            Assert.AreEqual(0, station.DepartureTrains.Count);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestTrainHasLeftMethodToReturnFalse()
        {
            RailwayStation station = new RailwayStation("TestStation");
            string trainInfo1 = "Train";
            string trainInfo2 = "Train1";
            station.NewArrivalOnBoard(trainInfo1);
            station.TrainHasArrived(trainInfo1);

            
            bool result = station.TrainHasLeft(trainInfo2);

            
            Assert.AreEqual(1, station.DepartureTrains.Count);
            Assert.IsFalse(result);
        }
    }
}