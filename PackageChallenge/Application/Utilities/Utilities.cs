using Domain;
using System;
using System.Collections.Generic;
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

                    for (int j = 2; j < subs.Length; i++)
                    {
                        Item newItem = new Item() {
                            Index = Convert.ToInt32(subs[0]),
                            Weight = Convert.ToDecimal(subs[2]),
                            Cost = Convert.ToDecimal(subs[3])
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
