using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test1Uge34;

namespace UnitTests
{
    [TestClass]
    public class TestPerson
    {
        // TODO: Test invalid values in constructor
        [TestMethod]
        public void TestConstructorValidValues()
        {
            string expectedName = "Some Guy";
            double expectedWeight = 87;
            double expectedHeight = 1.85;

            Person person = new Person(expectedName, expectedWeight, expectedHeight);

            string actualName = person.Name;
            double actualWeight = person.Weight;
            double actualHeight = person.Height;

            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedWeight, actualWeight);
            Assert.AreEqual(expectedHeight, actualHeight);
        }

        [TestMethod]
        public void TestBMI()
        {
            string name = "Some Guy";
            double weight = 87;
            double height = 1.85;
            Person person = new Person(name, weight, height);

            double expectedBMI = weight / (height * height);
            double actualBMI = person.BMI();

            Assert.AreEqual(expectedBMI, actualBMI);
        }

        // TODO: Test GiveWeightAdvice()
    }
}
