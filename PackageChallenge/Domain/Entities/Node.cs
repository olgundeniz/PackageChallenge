using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Node
    {
        // Upper Bound: Best case
        // (Fractional Knapsck)
        public float ub;

        // Lower Bound: Worst case
        // (0/1)
        public float lb;

        // Level of the node in
        // the decision tree
        public int level;

        // Stores if the current
        // item is selected or not
        public bool flag;

        // Total Value: Stores the
        // sum of the values of the
        // items included
        public float tv;

        // Total Weight: Stores the sum of
        // the weights of included items
        public float tw;
        public Node() { }
        public Node(Node cpy)
        {
            this.tv = cpy.tv;
            this.tw = cpy.tw;
            this.ub = cpy.ub;
            this.lb = cpy.lb;
            this.level = cpy.level;
            this.flag = cpy.flag;
        }
    }
}
