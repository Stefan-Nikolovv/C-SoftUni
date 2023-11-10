using System.Linq;
using NUnit.Framework;

namespace RobotFactory.Tests
{
    public class FactoryTests
    {
        private Factory factory;
        private Supplement supplement;
        [SetUp]
        public void Setup()
        {
            factory = new Factory("Ivan", 10);
        }

        [Test]
        public void ContrutorShouldWorkProperly()
        {
            string expectedName = "Ivan";
            int expectedCount = 10;

            Assert.AreEqual(expectedName, factory.Name);
            Assert.AreEqual(expectedCount, 10);
            Assert.IsNotNull(factory.Robots);
            Assert.IsNotNull(factory.Supplements);
        }
        [Test]
        public void NamePropertyShouldSetProperly()
        {
            string expectedName = "Peter";
            factory.Name = expectedName;

            Assert.AreEqual(expectedName, factory.Name);
        }
        [Test]
        public void CapaciryPropertyShouldSetProperly()
        {
            int expectedCapacity = 10;
            factory.Capacity = expectedCapacity;

            Assert.AreEqual(expectedCapacity, factory.Capacity);
        }
        [Test]
        public void ProcudeRobotShouldAddRobotToInnerCollection()
        {
            Robot expectedRobot = new Robot("T14", 123.25789, 25);
            string expectedMessage = $"Produced --> " +
                $"Robot model: {expectedRobot.Model} IS: {expectedRobot.InterfaceStandard}, Price: {expectedRobot.Price:f2}";

            string actualMessage = factory.ProduceRobot(expectedRobot.Model, expectedRobot.Price, expectedRobot.InterfaceStandard);
            Robot actualRobot = factory.Robots.Single();
            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual(expectedRobot.Model, actualRobot.Model);
            Assert.AreEqual(expectedRobot.Price, actualRobot.Price);
            Assert.AreEqual(expectedRobot.InterfaceStandard, actualRobot.InterfaceStandard);
        }
        [Test]
      
        public void ProcudeRobotShouldNotAddRobotToInnerCollection()
        {
            factory.Capacity = 0;
            string expectedMessage = $"The factory is unable to produce more robots for this production day!";
            string actualMessage = factory.ProduceRobot("Pesho", 25.452, 25);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void ProduceSupplementAddCorrectInput()
        {
            Supplement expectedSuppliment = new("Protein", 250);
            string expectedMessage = $"Supplement: {expectedSuppliment.Name} IS: {expectedSuppliment.InterfaceStandard}";

            string actualMessage = factory.ProduceSupplement(expectedSuppliment.Name, expectedSuppliment.InterfaceStandard);

            Supplement actualSuppliment = factory.Supplements.Single();

            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual(expectedSuppliment.Name, actualSuppliment.Name);
            Assert.AreEqual(expectedSuppliment.InterfaceStandard, actualSuppliment.InterfaceStandard);
            

        }

        [Test]
        public void UpgradeMethodWorkCorreclyAndReturnFalse()
        {
            Robot expectedRobot = new("T14", 123.25789, 250);
            Supplement expectedSuppliment = new("Protein", 250);
            _ = factory.UpgradeRobot(expectedRobot, expectedSuppliment);
            bool actualResult = factory.UpgradeRobot(expectedRobot, expectedSuppliment);

            Supplement actual = expectedRobot.Supplements.Single();

            Assert.IsFalse(actualResult);

        }
        [Test]
        public void UpgradeMethodWorkCorreclyAndReturnTrue()
        {
            Robot expectedRobot = new("T14", 123.25789, 250);
            Supplement expectedSuppliment = new("Protein", 250);

            bool actualResult = factory.UpgradeRobot(expectedRobot, expectedSuppliment);

            Supplement actual = expectedRobot.Supplements.Single();

            Assert.True(actualResult);
            Assert.AreEqual(expectedSuppliment.Name, expectedSuppliment.Name);
            Assert.AreEqual(expectedSuppliment.InterfaceStandard, expectedSuppliment.InterfaceStandard);
        }
        [Test]
        public void UpgradeMethodWorkCorreclyAndReturnFalseWhenInterFaceStandart()
        {
            Robot expectedRobot = new("T14", 123.25789, 250);
            Supplement expectedSuppliment = new("Protein", 250 + 1);

            bool actualResult = factory.UpgradeRobot(expectedRobot, expectedSuppliment);



            Assert.False(actualResult);
            
        }
        [Test]
        public void RobotShouldRetrunCorrectRobot()

        {
            Robot expectedRobot = new("T14", 546, 250);
            factory.ProduceRobot(expectedRobot.Model, expectedRobot.Price, expectedRobot.InterfaceStandard);
            factory.ProduceRobot("t15", 126, expectedRobot.InterfaceStandard);
            factory.ProduceRobot("P15", 700, expectedRobot.InterfaceStandard);

            Robot actualRobot = factory.SellRobot(655);

            Assert.AreEqual(expectedRobot.Model, actualRobot.Model);
        }
        [Test]
        public void SellRobotShouldReturnNullIsPriceIsTooLow()

        {
            Robot expectedRobot = new("T14", 546, 250);
            factory.ProduceRobot(expectedRobot.Model, expectedRobot.Price, expectedRobot.InterfaceStandard);
            factory.ProduceRobot("t15", 126, expectedRobot.InterfaceStandard);
            factory.ProduceRobot("P15", 700, expectedRobot.InterfaceStandard);

            Robot actualRobot = factory.SellRobot(124);

            Assert.IsNull(actualRobot);
        }
        [Test]
        public void SellRobotShouldReturnNullIsCollectionOfRobotsIsEmpty()

        {
            Robot actualRobot = factory.SellRobot(124);

            Assert.IsNull(actualRobot);
        }


    }
    
}