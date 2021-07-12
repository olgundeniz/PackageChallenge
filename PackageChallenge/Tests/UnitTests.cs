using System;
using System.Collections.Generic;
using System.IO;
using Application.Utilities;
using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void Utilities_ReadInput_InitializesCorrectInput()
        {
            List<int> maxWeights;
            List<int> expectedMaxWeights = new List<int> { 81, 8, 75, 56};

            var itemLists = new List<List<Item>>();
            var expectedItemLists = new List<List<Item>>();
            var expectedListByLine = new List<Item>();

            var newItem1 = new Item() { Index=1, Weight= 53.38m , Cost=45 };
            var newItem2 = new Item() { Index = 2, Weight = 88.62m, Cost = 98 };
            var newItem3 = new Item() { Index = 2, Weight = 15.3m, Cost = 34 };

            expectedItemLists.Add(new List<Item>() { newItem1, newItem2 });
            expectedItemLists.Add(new List<Item>() { newItem3 });

            string inputFilePath = @".\example_input";

            itemLists = Utilities.ReadInput(inputFilePath, out maxWeights);

            Assert.AreEqual(expectedMaxWeights[0], maxWeights[0]);
            Assert.AreEqual(expectedMaxWeights[1], maxWeights[1]);
            Assert.AreEqual(expectedMaxWeights[2], maxWeights[2]);
            Assert.AreEqual(expectedMaxWeights[3], maxWeights[3]);
        }
    }
}
