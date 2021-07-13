using System;
using System.Collections.Generic;
using System.IO;
using Application.Utilities;
using com.mobiquity.packer;
using Domain;
using Domain.Entities.Exceptions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void CheckMaxWeightOfPackage_ThrowsException()
        {
            List<int> maxWeights = new List<int> {1,2,3,4,101 };

            APIException ex = Assert.Throws<APIException>(() => Packer.CheckMaxWeightOfPackage(maxWeights));

            Assert.AreEqual($"Exception occured: {ExceptionMessage.MaxWeightOfPackage}", ex.Message);
        }
        [Test]
        public void CheckMaxItemCount_ThrowsException()
        {
            var itemLists = new List<List<Item>>();
            var newItem1 = new Item() { Index = 1, Weight = 53.38f, Cost = 45 };
            var newItem2 = new Item() { Index = 2, Weight = 88.62f, Cost = 98 };
            var newItem3 = new Item() { Index = 3, Weight = 78.48f, Cost = 3 };

            itemLists.Add(new List<Item>() { newItem1, newItem2 });
            itemLists.Add(new List<Item>() { newItem1, newItem2, newItem3, newItem1, newItem2, newItem3, newItem1, newItem2, newItem3 ,
                newItem1, newItem2, newItem3, newItem1, newItem2, newItem3, newItem1
            });

            APIException ex = Assert.Throws<APIException>(() => Packer.CheckMaxItemCount(itemLists));

            Assert.AreEqual($"Exception occured: {ExceptionMessage.MaxItemCount}", ex.Message);
        }
        [Test]
        public void CheckMaxWeightAndCostOfItem_ThrowsException()
        {
            var itemLists = new List<List<Item>>();
            var newItem1 = new Item() { Index = 1, Weight = 53.38f, Cost = 45 };
            var newItem2 = new Item() { Index = 2, Weight = 88.62f, Cost = 98 };
            var newItem3 = new Item() { Index = 3, Weight = 78.48f, Cost = 101 };

            itemLists.Add(new List<Item>() { newItem1, newItem2 });
            itemLists.Add(new List<Item>() { newItem1, newItem2, newItem3, newItem1, newItem2});

            APIException ex = Assert.Throws<APIException>(() => Packer.CheckMaxWeightAndCostOfItem(itemLists));

            Assert.AreEqual($"Exception occured: {ExceptionMessage.MaxWeightAndCostOfItem}", ex.Message);
        }
        [Test]
        public void Utilities_ReadInput_InitializesCorrectInput()
        {
            List<int> maxWeights;
            List<int> expectedMaxWeights = new List<int> { 81, 8, 75, 56};

            var itemLists = new List<List<Item>>();
            var expectedItemLists = new List<List<Item>>();
            var expectedListByLine = new List<Item>();

            //arrange
            var newItem1 = new Item() { Index=1, Weight= 53.38f , Cost=45 };
            var newItem2 = new Item() { Index = 2, Weight = 88.62f, Cost = 98 };
            var newItem3 = new Item() { Index = 3, Weight = 78.48f, Cost = 3 };
            var newItem4 = new Item() { Index = 4, Weight = 72.30f, Cost = 76 };
            var newItem5 = new Item() { Index = 5, Weight =30.18f, Cost = 9 };
            var newItem6 = new Item() { Index = 6, Weight = 46.34f, Cost = 48 };

            var newItem7 = new Item() { Index = 1, Weight = 15.3f, Cost = 34 };

            expectedItemLists.Add(new List<Item>() { newItem1, newItem2, newItem3, newItem4, newItem5, newItem6 });
            expectedItemLists.Add(new List<Item>() { newItem7 });

            string inputFilePath = @".\example_input";

            //act
            itemLists = Utilities.ReadInput(inputFilePath, out maxWeights);

            //assert max weights
            Assert.AreEqual(expectedMaxWeights[0], maxWeights[0]);
            Assert.AreEqual(expectedMaxWeights[1], maxWeights[1]);
            Assert.AreEqual(expectedMaxWeights[2], maxWeights[2]);
            Assert.AreEqual(expectedMaxWeights[3], maxWeights[3]);

            //assert items in the test file
            for (int i = 0; i < expectedItemLists.Count; i++)
            {
                for (int j = 0; j < expectedItemLists[i].Count; j++)
                {
                    Assert.AreEqual(expectedItemLists[i][j], itemLists[i][j]);
                }
            }
        }
        [Test]
        public void Pack_CorrectResult()
        {
            string inputFilePath = @".\example_input";

            string result = Packer.Pack(inputFilePath);
        }
    }
}
