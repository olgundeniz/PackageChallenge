using Application.Utilities;
using Domain;
using Domain.Entities;
using Domain.Entities.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Wintellect.PowerCollections;

[assembly: InternalsVisibleTo("Tests")]
namespace com.mobiquity.packer
{
    public class Packer
    {
        private static ILogger Log;
        private static int size;
        private static float capacity;
        public static string Pack(string filePath, ILogger logger = null)
        {
            Log = logger;
            List<List<Item>> inputList = new List<List<Item>>();
            List<int> maxWeights = new List<int>();

            inputList = Utilities.ReadInput(filePath, out maxWeights, Log);

            CheckMaxWeightOfPackage(maxWeights);

            CheckMaxItemCount(inputList);

            CheckMaxWeightAndCostOfItem(inputList);

            StringBuilder sb = new StringBuilder();
            try
            {
                for (int i = 0; i < inputList.Count; i++)
                {
                    List<Item> subList = inputList[i];
                    size = subList.Count;
                    capacity = maxWeights[i];

                    sb.AppendLine(solve(subList));
                }
            }
            catch (Exception ex)
            {
                Log?.LogError($"Exception occured");
                Log?.LogError($"Exception message: {ex.Message}");
                throw new APIException($"Exception occured: {ex.Message}");
            }
            
            return sb.ToString();
        }

        internal static void CheckMaxWeightAndCostOfItem(List<List<Item>> inputList)
        {
            foreach (var subList in inputList)
            {
                if (subList.Exists(x=>x.Weight > 100 || x.Cost > 100))
                {
                    Log?.LogError($"Exception occured");
                    Log?.LogError($"Exception message: {ExceptionMessage.MaxWeightAndCostOfItem}");
                    throw new APIException($"Exception occured: {ExceptionMessage.MaxWeightAndCostOfItem}");
                }
            }
        }

        internal static void CheckMaxItemCount(List<List<Item>> inputList)
        {
            foreach (var subList in inputList)
            {
                if (subList.Count > 15)
                {
                    Log?.LogError($"Exception occured");
                    Log?.LogError($"Exception message: {ExceptionMessage.MaxItemCount}");
                    throw new APIException($"Exception occured: {ExceptionMessage.MaxItemCount}");
                }
            }            
        }

        internal static void CheckMaxWeightOfPackage(List<int> maxWeights)
        {
            if(maxWeights.Exists(x => x > 100))
            {
                Log?.LogError($"Exception occured");
                Log?.LogError($"Exception message: {ExceptionMessage.MaxWeightOfPackage}");
                throw new APIException($"Exception occured: {ExceptionMessage.MaxWeightOfPackage}");
            }
        }

        // Function to calculate upper bound
        // (includes fractional part of the items)
        static float upperBound(float tv, float tw,
                                int idx, List<Item> arr)
        {
            float value = tv;
            float weight = tw;
            for (int i = idx; i < size; i++)
            {
                if (weight + arr[i].Weight
                    <= capacity)
                {
                    weight += arr[i].Weight;
                    value -= arr[i].Cost;
                }
                else
                {
                    value -= (float)(capacity
                                     - weight)
                             / arr[i].Weight
                             * arr[i].Cost;
                    break;
                }
            }
            return value;
        }

        // Calculate lower bound (doesn't
        // include fractional part of items)
        static float lowerBound(float tv, float tw,
                                int idx, List<Item> arr)
        {
            float value = tv;
            float weight = tw;
            for (int i = idx; i < size; i++)
            {
                if (weight + arr[i].Weight
                    <= capacity)
                {
                    weight += arr[i].Weight;
                    value -= arr[i].Cost;
                }
                else
                {
                    break;
                }
            }
            return value;
        }

        static void assign(Node a, float ub, float lb,
                           int level, bool flag,
                           float tv, float tw)
        {
            a.ub = ub;
            a.lb = lb;
            a.level = level;
            a.flag = flag;
            a.tv = tv;
            a.tw = tw;
        }

        public static string solve(List<Item> arr)
        {
            // Sort the items based on the
            // profit/weight ratio
            arr.Sort(new sortByRatio());

            Node current, left, right;
            current = new Node();
            left = new Node();
            right = new Node();

            // min_lb -> Minimum lower bound
            // of all the nodes explored

            // final_lb -> Minimum lower bound
            // of all the paths that reached
            // the final level
            float minLB = 0, finalLB
                             = Int32.MaxValue;
            current.tv = current.tw = current.ub
                = current.lb = 0;
            current.level = 0;
            current.flag = false;

            // Priority queue to store elements
            // based on lower bounds

            OrderedBag<Node> pq
                = new OrderedBag<Node>(
                    new sortByC());

            // Insert a dummy node
            pq.Add(current);

            // curr_path -> Boolean array to store
            // at every index if the element is
            // included or not

            // final_path -> Boolean array to store
            // the result of selection array when
            // it reached the last level
            bool[] currPath = new bool[size];
            bool[] finalPath = new bool[size];

            while (pq.Count > 0)
            {
                current = pq.GetFirst();
                pq.RemoveFirst();
                if (current.ub > minLB
                    || current.ub >= finalLB)
                {
                    // if the current node's best case
                    // value is not optimal than minLB,
                    // then there is no reason to
                    // explore that node. Including
                    // finalLB eliminates all those
                    // paths whose best values is equal
                    // to the finalLB
                    continue;
                }

                if (current.level != 0)
                    currPath[current.level - 1]
                        = current.flag;

                if (current.level == size)
                {
                    if (current.lb < finalLB)
                    {
                        // Reached last level
                        for (int i = 0; i < size; i++)
                            finalPath[arr[i].Index-1]
                                = currPath[i];
                        finalLB = current.lb;
                    }
                    continue;
                }

                int level = current.level;

                // right node -> Exludes current item
                // Hence, cp, cw will obtain the value
                // of that of parent
                assign(right, upperBound(current.tv,
                                         current.tw,
                                         level + 1, arr),
                       lowerBound(current.tv, current.tw,
                                  level + 1, arr),
                       level + 1, false,
                       current.tv, current.tw);

                if (current.tw + arr[current.level].Weight
                    <= capacity)
                {

                    // left node -> includes current item
                    // c and lb should be calculated
                    // including the current item.
                    left.ub = upperBound(
                        current.tv
                            - arr[level].Cost,
                        current.tw
                            + arr[level].Weight,
                        level + 1, arr);
                    left.lb = lowerBound(
                        current.tv
                            - arr[level].Cost,
                        current.tw
                            + arr[level].Weight,
                        level + 1,
                        arr);
                    assign(left, left.ub, left.lb,
                           level + 1, true,
                           current.tv - arr[level].Cost,
                           current.tw
                               + arr[level].Weight);
                }

                // If the left node cannot
                // be inserted
                else
                {

                    // Stop the left node from
                    // getting added to the
                    // priority queue
                    left.ub = left.lb = 1;
                }

                // Update minLB
                minLB = Math.Min(minLB, left.lb);
                minLB = Math.Min(minLB, right.lb);

                if (minLB >= left.ub)
                    pq.Add(new Node(left));
                if (minLB >= right.ub)
                    pq.Add(new Node(right));
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                if (finalPath[i])
                {
                    sb.Append(i+1 + ",");
                }
            }

            if (string.IsNullOrEmpty(sb.ToString()))
                return "-";
            else
                sb.Remove(sb.Length-1, 1);

            return sb.ToString();
        }
    }
}
