namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConstuctor()
        {
            Device device = new Device(50);

            Assert.AreEqual(device.MemoryCapacity, 50);
            Assert.AreEqual(device.AvailableMemory, 50);
            Assert.AreEqual(device.Photos, 0);
            Assert.AreEqual(device.Applications.Count, 0);

        }

        [Test]
        public void TestTakePhotoMethodToReturnFalse()
        {
            Device device = new Device(50);
            bool actual = device.TakePhoto(60);
            bool expected = false;
            Assert.AreEqual(expected, actual);


        }

        [Test]
        public void TestTakePhotoMethodToReturnTrue()
        {
            Device device = new Device(50);
            bool actual = device.TakePhoto(50);
            bool expected = true;
            Assert.AreEqual(device.AvailableMemory, 0);
            Assert.AreEqual(device.Photos, 1);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestInstallAppMethodToThrowError()
        {
            Device device = new Device(50);
            Assert.Throws<InvalidOperationException>(() => device.InstallApp("Java", 60));
        }
        [Test]
        public void TestInstallAppMethodToWorkProperly()
        {
            Device device = new Device(50);

            var actual = device.InstallApp("Java", 30);
            var expected = $"Java is installed successfully. Run application?";

            Assert.AreEqual(20, device.AvailableMemory);
            Assert.AreEqual(1, device.Applications.Count);
            Assert.AreEqual(expected, actual);


        }
        [Test]
        public void TestFormatDeviceMethodToWorkProperly()
        {
            Device device = new Device(50);

            device.FormatDevice();


            Assert.AreEqual(50, device.AvailableMemory);
            Assert.AreEqual(50, device.MemoryCapacity);
            Assert.AreEqual(0, device.Applications.Count);
            Assert.AreEqual(0, device.Photos);


        }

        [Test]
        public void TestGetDeviceStatusMethodToWorkProperly()
        {
            Device device = new Device(150);
            StringBuilder stringBuilder = new StringBuilder();

            var expectedDeviceMemory = 150;
            var expectedDeviceAvailableMemory = 50;

            device.TakePhoto(50);
            device.InstallApp("Java", 25);
            device.InstallApp("JavaScript", 25);

            stringBuilder.AppendLine($"Memory Capacity: {expectedDeviceMemory} MB, Available Memory: {expectedDeviceAvailableMemory} MB");
            stringBuilder.AppendLine($"Photos Count: 1");
            stringBuilder.AppendLine($"Applications Installed: Java, JavaScript");

            var expected = stringBuilder.ToString().TrimEnd();
            var actual = device.GetDeviceStatus();





            Assert.AreEqual(actual, expected);






        }
    }
}