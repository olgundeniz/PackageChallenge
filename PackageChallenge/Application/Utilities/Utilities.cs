using Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities
{
    public static class Utilities
    {
        public static List<List<Item>> ReadInput(string filePath, out List<int> maxWeights)
        {
            var input = new List<List<Item>>();
            maxWeights = new List<int>();

            try
            {
                string[] lines = System.IO.File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] subs = lines[i].Split(" ");

                    //Add max weight of each line to the list
                    maxWeights.Add(Convert.ToInt32(subs[0]));

                    var inputOfEachLine = new List<Item>();

                    for (int j = 2; j < subs.Length; j++)
                    {
                        string[] subsItem = subs[j].Split(',');
                        Item newItem = new Item() {
                            Index = Convert.ToInt32(subsItem[0].Remove(0,1)),
                            Weight = float.Parse(subsItem[1], new CultureInfo("en-US")),
                            Cost = float.Parse(subsItem[2].Substring(1, subsItem[2].Length-2), new CultureInfo("en-US"))
                        };

                        inputOfEachLine.Add(newItem);
                    }

                    input.Add(inputOfEachLine);
                }
            }
            catch (Exception ex)
            {
                //TODO: log the stack trace
                throw new APIException("Exception occured while reading from input file. Details:" + " " + ex.Message);
            }

            return input;
        }
    }
}
